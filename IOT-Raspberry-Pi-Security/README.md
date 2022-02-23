# IOT 2021 Exam - Raspberry Pi security

The subject of this project is security. The main part is in the Raspberry Pi. Raspberry Pi would have a distance sensor, which would be triggered if anyone comes by it. Once the sensor is triggered, the picture will be captured. This picture then will be sent to the API, where it will be saved to a Firebase Bucket with a reference in MongoDB. Once it is saved, the MQTT message will be published with the data and the picture. Thanks to this MQTT message - another program running on Raspberry Pi would light up a led light, informing user about the sensor being triggered. Next to the led light there is a button to turn off all lights. Once the MQTT message is sent, the data is also sent via socket. This allows us to have live data on the Client - the Admin panel. In this admin panel user can see all of the captures pictures from all of the dates. In order to add sensor to the user’s profile, there is a button next to the sensor, once the user presses the button, the sensor’s id appears in the ‘pair’ section. The client and the API also has JWT authentication system.

### Technical overview
 - For Raspberry Pi, I used `Python` as the main programming language. I also used the `GPIOZero` library to work with sensors and led lights.
 - For the API (Backend) I used the `NEST.js` framework.
 - For the Client (Frontend) I used `Angular` framework.
 - For `Authentication JWT token system` is used.
 - As a Database to store sensor’s observed data i used `MongoDB`
 - The Messenger broker I chose is `CloudMQTT`.
 - Pictures are saved in `Firebase Bucket`.

### Communication
![Arcitecture](https://i.imgur.com/HIs4Mhw.png)

- Documentation can be found here: https://docs.google.com/document/d/1nQIiajgT3rt7SHqjqeZmmBTNT4Z4SqkbKS2YlZLJ-7g
- Presentation for the project can be found here: https://docs.google.com/presentation/d/1FWgOmTWScVHguE-zmzobxv1TCzpmLdWPPMsTGNsZYTA
- Presentation for the technologies used (CloudMQTT, Socket.io) can be found here: https://docs.google.com/presentation/d/1e89J5EDcVA8h4Np7zXPMhNoTDvn2HkcCOk9fSSHn9DA
