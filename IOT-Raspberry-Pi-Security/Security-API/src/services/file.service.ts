import {Injectable} from "@nestjs/common";
import firebase from "firebase";

/**
 * Injectable class FileService
 */
@Injectable()
export class FileService {
    private storageRef: firebase.storage.Reference;
    private MOTION_SENSOR_PATH = 'motion-sensor/';

    /**
     * FileService constructor
     * Initializes firebase storage
     */
    constructor() {

        // Firebase configuration TODO move to enviroment
        const firebaseConfig = {
            apiKey: "AIzaSyDz34BERKX8N2UwsO9IjB7qt7lz4t3_6Tw",
            authDomain: "iot-security-exam-2021.firebaseapp.com",
            projectId: "iot-security-exam-2021",
            storageBucket: "iot-security-exam-2021.appspot.com",
            messagingSenderId: "208267959563",
            appId: "1:208267959563:web:9eea394caba1c77b34d60e",
            measurementId: "G-JFYVTTQ3G5"
        };
        // Initialize Firebase
        firebase.initializeApp(firebaseConfig);

        // Get a reference to the storage service, which is used to create references in your storage bucket
        this.storageRef = firebase.storage().ref();
    }

    /**
     * Saves base64 image to Firebase Storage, returns full path to the image
     * @param base64 string - base64 version of the image
     * @param sensorId string - picture will be places in this directory
     * @param date Date - Picture will have date's name
     * @return Promise<{}> - a promise containing full path to the picture in Firebase Storage
     */
    uploadPicture(base64: string, sensorId: string, date: Date): Promise<any> {
        // Create a reference to 'motion-sensor/123/2021-09-12.jpg'
        const imageRef = this.storageRef.child(this.MOTION_SENSOR_PATH + sensorId + '/' + date.toString() + '.jpg');

        // Return full path of the picture and download URL
        return imageRef.putString(base64, 'base64').then((uploadSnapshot) => {
            return uploadSnapshot.ref.getDownloadURL().then(downloadUrl => {
                return {
                    fullSavedPicturePath: uploadSnapshot.metadata.fullPath,
                    downloadUrl: downloadUrl
                };
            });

        });
    }
}
