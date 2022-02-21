# Taxi-Finder android application app made for Mobile Development course Exam

### App overview

The current version of the app contains a log-in system that is based on Firebase authentication system, two map activities, one for driver and one for a customer. Both users are able to upload pictures to FirebaseStorage, all information is stored in Firebase real-time database and communication between users are mainly depended on EventValueListeners on Database value changes. The project uses three third-party libraries, the first one is for pushing images into ImageView only with the URL of the image, it is called Glide. The second one is for drawing routes between two locations, it is called ‘Google-Directions-Android’, and a third library we used was GeoFire, it is a library that allowed us to store and query a set of keys based on geographic location (latitude and longitude). The app is using three Google APIs, Directions API - used for finding directions between two locations, Maps SDK -  for google map, and Places API - for searching places and autocompletion.


![Demo Picture of the app](https://i.imgur.com/a05pG32.png)

A complete report can be found here: https://docs.google.com/document/d/11GFVKL1xpEza3AIo_y7G2GxTMhkMNtdf4Hz0FEgHFHE
