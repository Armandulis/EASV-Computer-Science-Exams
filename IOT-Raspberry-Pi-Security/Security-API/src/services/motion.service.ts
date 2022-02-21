import {Inject, Injectable} from "@nestjs/common";
import {InjectModel} from "@nestjs/mongoose";
import {Motion, MotionDocument} from "../models/motion.schema";
import {Model} from "mongoose";
import {ClientMqtt} from "@nestjs/microservices";
import {FileService} from "./file.service";

/**
 * Injectable class MotionService
 */
@Injectable()
export class MotionService {

    /**
     * MotionService constructor
     * @param client Mqtt - client
     * @param motionModel Model - Motion model
     * @param fileService FileService - Handles files
     */
    constructor(
        @Inject('MQTT_CLIENT') private client: ClientMqtt,
        @InjectModel(Motion.name)
        private motionModel: Model<MotionDocument>,
        private fileService: FileService
    ) {
    }

    /**
     * Get Latest motion values
     * @return Promise<Motion> - The latest Motion sensor values
     */
    async getLatest(): Promise<Motion> {
        return this.motionModel.findOne().sort({measurementTime: -1}).exec();
    }

    /**
     * Finds specific motion sensor
     * @param sensorId string - Id of specific motion sensor
     * @return Promise<Motion> - Motion sensor values for the sensor with id that was provided
     */
    async getById(sensorId: string): Promise<Motion> {
        return this.motionModel.findOne({sensorId: sensorId}).exec();
    }

    /**
     * Saves newly triggered motion sensor values
     * @param motionData Motion - Detected motion
     * @return Promise<Motion> - Saved motion sensor
     */
    async motionDetected(motionData: Motion): Promise<Motion> {

        // Upload picture
        return this.fileService.uploadPicture(
            motionData.base64Picture,
            motionData.sensorId,
            motionData.measurementTime
        ).then(pictureData => {

            // Save picture's path and download url
            motionData.fullSavedPicturePath = pictureData.fullSavedPicturePath;
            motionData.downloadUrl = pictureData.downloadUrl;

            // Do not store base64 in database as well
            motionData.base64Picture = undefined;

            // Save model and return it
            const motionModel = new this.motionModel(motionData);
            this.emitMessage(MqttPatterns.MOTION_DETECTED, motionData);
            return motionModel.save();
        });
    }

    /**
     * Removes all sensor's saved values
     * @param sensorId string - id of sensor who's all values will be removed
     */
    async deleteMotionSensorValues(sensorId: string) {
        const sensorValues = await this.motionModel.find({sensorId: sensorId}).exec();
        sensorValues.forEach(motionSensor => {
            this.motionModel.remove(motionSensor).exec();
        });
    }

    /**
     * Sends a message to cloud mqtt about triggered motion sensor
     * @param pattern sring - pattern of the message
     * @param motion Motion - that will be emitted
     */
    emitMessage(pattern: string, motion: Motion): void {
        this.client.emit(pattern, JSON.stringify(motion)).toPromise().then();
    }

    /**
     * Finds all motions by sensorId
     * @param sensorId string - Motion's sensor ids
     * @return Promise<Motion[]> - Found motions
     */
    getAllById(sensorId: string): Promise<Motion[]> {
        return this.motionModel.find({sensorId: sensorId}).exec();
    }
}
