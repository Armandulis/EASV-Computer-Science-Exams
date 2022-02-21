import {Body, Controller, Delete, Get, Param, Post, Put, Query, UseGuards} from "@nestjs/common";
import {MotionService} from "../services/motion.service";
import {Motion} from "../models/motion.schema";
import {JwtAuthGuard} from "./auth/guards/jwt-auth.guard";
import {SocketService} from "../services/socket.service";
import {Ctx, MessagePattern, MqttContext, Payload} from "@nestjs/microservices";
import {Server} from "socket.io";
import {OnGatewayInit} from "@nestjs/websockets";

/**
 * Controller MotionController
 */
@Controller('motion')
export class MotionController implements OnGatewayInit {

    /**
     * MotionController constructor
     * @param service MotionService - Handles Motion
     * @param socketService SocketService - Communicate with client via socket.io
     */
    constructor(private readonly service: MotionService,
                private readonly socketService: SocketService) {
    }

    /**
     * Set server to service after it was initialized
     * @param server Server - Socket service's server
     */
    afterInit(server: Server) {
        this.socketService.server = server;
    }

    /**
     * PUT request to /motion
     * @param dto @Body()
     * @return saved motion
     */
    @Put()
    update(@Body() dto: Motion): Promise<Motion> {
        return this.service.motionDetected(dto);
    }

    /**
     * POST request to /motion
     * @param dto @Body()
     * @return saved motion
     */
    @Post()
    post(@Body() dto: Motion){
        return this.service.motionDetected(dto);
    }

    /**
     * GET request to /motion?sensorId={sensorId}
     * @param query @Query()
     * @return sensor values by id
     */
    @UseGuards(JwtAuthGuard)
    @Get()
    getById(@Query() query): Promise<Motion> {
        const sensorId = query.sensorId;

        // Just return null if no sensor id is given
        if (sensorId === undefined) {
            return null;
        }
        return this.service.getById(sensorId);
    }

    /**
     * GET request to /motion/all?sensorId={sensorId}
     * @param query @Query()
     * @return sensor values by id
     */
    @UseGuards(JwtAuthGuard)
    @Get(':all')
    getAllById(@Query() query): Promise<Motion[]> {
        const sensorId = query.sensorId;

        // Just return null if no sensor id is given
        if (sensorId === undefined) {
            return null;
        }
        return this.service.getAllById(sensorId);
    }

    /**
     * GET request to /motion/latest
     * @param param @Param()
     * @return latest motion values
     */
    // @UseGuards(JwtAuthGuard) // TODO uncomment this
    @Get(':latest')
    getLatest(@Param() param): Promise<Motion> {
        return this.service.getLatest();
    }

    /**
     * PUT request to /motion
     * @param query @Query()
     */
    @UseGuards(JwtAuthGuard)
    @Delete()
    delete(@Query() query): string {
        const sensorId = query.sensorId;
        this.service.deleteMotionSensorValues(sensorId);
        return `All motion sensor values were deleted for sensor with id: ${sensorId}`;
    }

    /**
     * POST request to /motion/pair
     * Emits sensor that is ready to be paired through socket
     * @param jsonSensorId @Body()
     * @param query @Query()
     */
    @Post(':pair')
    async pairSensor (@Body() jsonSensorId, @Query() query){
        const sensorId: string = query.sensorId;
        console.log(sensorId);
        this.socketService.server.emit('sensor/pair', sensorId);
    }
}
