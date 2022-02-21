import {WebSocketGateway, WebSocketServer} from "@nestjs/websockets";
import {SocketService} from "../../services/socket.service";
import {Server} from "socket.io";

/**
 * Class SocketGateway
 */
@WebSocketGateway({ cors: true })
export class SocketGateway {
    @WebSocketServer() public server: Server;

    /**
     * SocketGateway constructor
     * @param socketService SocketService - Handles socket initialization
     */
    constructor(private socketService: SocketService) {}

    /**
     * Sets Socket service's server
     * @param server Server
     */
    afterInit(server: Server) {
        this.socketService.server = server;
    }
}
