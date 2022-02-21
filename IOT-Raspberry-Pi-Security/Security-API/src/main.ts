import {NestFactory} from '@nestjs/core';
import {AppModule} from './app.module';
import {Transport} from "@nestjs/microservices";

async function bootstrap() {
    const app = await NestFactory.create(AppModule);

    // Set up cores
    app.enableCors({
        origin: '*',
    });

    // Set up MQTT
    app.connectMicroservice({
        transport: Transport.MQTT,
        options: {
            url: 'mqtts://driver.cloudmqtt.com:28692',
            username: 'guhpfdxf',
            password: 'jcceQ1eHsoAg',
        },
    });
    app.startAllMicroservices();

    // Because we are dealing with files, we will need to increase limit size
    const bodyParser = require('body-parser');
    app.use(bodyParser.json({limit: '50mb', extended: false}));
    app.use(bodyParser.urlencoded({limit: '50mb', extended: false}));

    // Start application on 3000 for localhost and PORT for live site
    await app.listen(process.env.PORT || 3000);
}

bootstrap();
