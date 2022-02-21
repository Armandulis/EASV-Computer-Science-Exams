import requests
from datetime import datetime
from time import sleep
from gpiozero import DistanceSensor, Button
from picamera import PiCamera
import base64

# Setup Camera and Distance sensor
camera = PiCamera()
camera.resolution = ( 1280, 720 )
sensor = DistanceSensor(18, 4 )
filePath = ''

# Set up Button
pairButoonPressed = False
pairButton = Button(2)

# Define Pair Button callback methods
def pairOff():
    print( 'letgo')
    global pairButoonPressed
    pairButoonPressed = False

def pairPressed():
    global pairButoonPressed
    if(not pairButoonPressed):
         r = requests.post( 'https://iot-2021-security-api.herokuapp.com/motion/pair?sensorId=' + sensorId)
         pairButoonPressed = True
         print('pressed')    

pairButton.when_pressed = pairPressed
pairButton.when_released = pairOff

# Method to easily get sensor's ID
def getSerial():
  # Extract serial from cpuinfo file
  cpuserial = "0000000000000000"
  try:
    f = open('/proc/cpuinfo','r')
    for line in f:
      if line[0:6]=='Serial':
        cpuserial = line[10:26]
    f.close()
  except:
    cpuserial = "ERROR000000000"
  return cpuserial
sensorId = getSerial()

# Take a picture once motion is detected
def motionDetected():
    currentTime = datetime.now()
    global filePath
    filePath = f'/home/pi/Desktop/captured-pictures/{currentTime}.jpeg'
    camera.capture( filePath )

# Keep loking for sensor triggers whiel the program is running
while True:

    #Sensor is triggered if distance is less than 50 cm
    if sensor.distance < 0.5:
        motionDetected()

        # Get image as base64 string
        with open(filePath, 'rb') as image_file:
            encoded_string = base64.b64encode(image_file.read())
            image_file.close()
        
        # Send a POST request to the API with newly captured Picture information
        payload = { 'sensorId': sensorId, 'measurementTime': datetime.now(), 'base64Picture':  encoded_string }
        r = requests.post( 'https://iot-2021-security-api.herokuapp.com/motion', payload)

        # Do not trigger sensor more than 0.5s
        sleep(0.5)
    