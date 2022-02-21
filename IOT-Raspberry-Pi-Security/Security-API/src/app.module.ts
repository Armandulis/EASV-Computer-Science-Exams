import {Module} from '@nestjs/common';
import {MongooseModule} from "@nestjs/mongoose";
import {Motion, MotionSchema} from "./models/motion.schema";
import {MotionController} from "./controllers/motion.controller";
import {ClientsModule, Transport} from "@nestjs/microservices";
import {MotionService} from "./services/motion.service";
import {SocketService} from "./services/socket.service";
import {User, UserSchema} from "./models/user.schema";
import {UserService} from "./services/user.service";
import {Auth, AuthSchema} from "./models/auth.schema";
import {AuthService} from "./services/auth.service";
import {LocalStrategy} from "./controllers/auth/local.strategy";
import {SocketGateway} from "./controllers/mqtt/socket.gateway";
import {LoginController} from "./controllers/login.controller";
import {MotionMqttController} from "./controllers/mqtt/motion.mqqt.controller";
import {JwtModule} from "@nestjs/jwt";
import {JwtStrategy} from "./controllers/auth/jwt.strategy";
import {FileService} from "./services/file.service";

@Module({
    imports: [
        ClientsModule.register([
            {
                name: 'MQTT_CLIENT',
                transport: Transport.MQTT,
                options: {
                    url: 'mqtts://driver.cloudmqtt.com:28692',
                    username: 'guhpfdxf',
                    password: 'jcceQ1eHsoAg',
                },
            },
        ]),
        MongooseModule.forRoot(
            'mongodb+srv://classroom:9Npt4YrmO6E1hxFt@cluster0.dg2lq.mongodb.net/pidata?retryWrites=true&w=majority',
        ),

        JwtModule.register({
            secret: 'IOT_EXAM_2021_RASP_SECURITY', // TODO: put in env
            signOptions: {
                expiresIn: '8h'
            }
        }),

        /** Models */
        MongooseModule.forFeature([
            {name: Motion.name, schema: MotionSchema},
            {name: User.name, schema: UserSchema},
            {name: Auth.name, schema: AuthSchema}
        ]),
    ],
    controllers: [
        MotionController,
        LoginController,
        MotionMqttController
    ],
    providers: [
        /** Services */
        SocketService,
        SocketGateway,
        MotionService,
        UserService,
        AuthService,
        LocalStrategy,
        JwtStrategy,
        FileService
    ],
    exports: [
        SocketService,
        AuthService
    ]
})

/**
 * Class AppModule
 */
export class AppModule {
}
