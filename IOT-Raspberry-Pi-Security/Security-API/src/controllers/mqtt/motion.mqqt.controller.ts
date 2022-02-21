import {Controller, Inject} from "@nestjs/common";
import {OnGatewayInit} from "@nestjs/websockets";
import {MotionService} from "../../services/motion.service";
import {ClientMqtt, Ctx, MessagePattern, MqttContext, Payload} from "@nestjs/microservices";
import {SocketService} from "../../services/socket.service";
import {Server} from "socket.io";
import {Motion} from "../../models/motion.schema";

/**
 * Class MotionMqttController
 */
@Controller()
export class MotionMqttController implements OnGatewayInit {

    /**
     * MotionMqttController constructor
     * @param client ClientMqqt - Handles MQQT messages
     * @param motionService MotionService - Handles motion
     * @param socketService SocketService - Communicate with client via socket.io
     */
    constructor(
        @Inject('MQTT_CLIENT') private client: ClientMqtt,
        private readonly motionService: MotionService,
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
     * Emits motion through socket when mqqt message is received
     * @param data Motion - Motion's data
     * @param context
     */
    @MessagePattern( MqttPatterns.MOTION_DETECTED )
    async motionDetected(@Payload() data: any, @Ctx() context: MqttContext) {
        const motionData: Motion = JSON.parse(data);
        this.socketService.server.emit(motionData.sensorId, motionData);
        this.client.emit(motionData.sensorId + '/detected', true).toPromise().then();
        return motionData;
    }
}
