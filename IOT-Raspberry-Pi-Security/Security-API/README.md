# Nest.js API - Security

### Firebase
The API was created using `Node.js`. This is the core of the whole project, almost all of the information is received/shared through this application. The raspberry Pi sends a base64 string picture to the API, where then it is saved to the `Firebase`. After we saved the picture, we are able to receive picture's download URL, which we will store in the `MongoDB`, together with the date and sensor's id.

![Picture upload Demo](https://i.imgur.com/LE7s8Ev.png)

### MQQT and Socket.io 
The Client and Raspbery Pi `signal-motion.py` receives information from the API. Signal-motion application doesn't directly receive information that the signal was trigger, instead API sends an MQTT message to the `CloudMQTT`, from there, any of the running signal-motion applications will receive this iformation. The Client on the other hand, receives information directly from the API via `Socket.io`. Socket.io helps to keep the client with up-to-date information, makes the client very responsive. The user is able to see signal picture right away when it's uploaded, thanks to socket.io, user doesn't need to refresh the page.

![Picture upload Demo](https://i.imgur.com/3F2A5Id.png)

### JWT Authentication
The API is also implemented with the `JWT authentication` system, the password hashing and Token generation happens in this API. The most crutial requests are protected with a guard, where you can only get a response back if you have a valid Token.
