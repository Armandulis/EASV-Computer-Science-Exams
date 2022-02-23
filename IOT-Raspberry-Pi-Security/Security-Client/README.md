# Angular Client - Security

The client has a very simple design, as it was not the main focus of the project. What was importat, was that the user would see up-to-date information right away. So that the user wouldn't need to refresh the page, to see if a motion was striggered. To achieve that i used a very powerful tool called `Socket.io`. Thanks to the socket.io, i was able to do just that. The data series for new events are signaled through socket.io, then we make a request to receive all of the motions and display them in the chart. Also security is important for the frontend - the token is stored in the sessionStorage, and the token is passed to the API with every request.

![Demo Listen for data series](https://i.imgur.com/RdagrZr.png)

The Devices are also paired via Socket.io, meaning that you can only pear the device for a very short time, which also improves security.

![Demo Listed for devices to pair](https://i.imgur.com/04s5TmV.png)
