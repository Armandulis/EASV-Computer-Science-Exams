# Raspberry Pi - Security
2 separate programs/raspberry pis should be running. One for `motion-sensor.py` and another one for `signal-motion.py`. 

`motion-sensor.py` is used with camera and sensor. There is also a button to pair device with admin account.

`signal-motion.py` is very simple, just used to inform user about triggered motion, also to inform other devices that the motion was noticed (by pressing a button).
### Devices used:
 - `Raspberry Pi 4` - as the main hardware it also comes with:
   - `An ultrasonic distance sensor` - to detect if anyone comes by the sensor;
   - `Camera` for capturing pictures;
   - `Another program` - would be responsible for lighting up the led lights when the sensor triggers. In theory, there could be many of these programs running in different locations for example;

![Raspberry Pi](https://i.imgur.com/IXGtpQo.png)
![Raspberry Pi Top](https://i.imgur.com/5xjtYZm.png)
