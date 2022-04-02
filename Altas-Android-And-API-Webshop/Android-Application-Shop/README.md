# Altas Android application
![Design](https://user-images.githubusercontent.com/32477822/161393668-8d21d27e-cd5d-4d96-9403-5f6ce876ed2a.png)

### Android development
Developers began work by creating Fragments. Each fragment had it’s on lifecycle, layout and almost all fragments had its own ViewModel. We tried to always get values for the layout from String. Values instead of using raw strings. We also tried to use Material Design rules. For this project, we used the Glider library. This library was very useful, it allowed for us to parse image URL to an image view. This was perfect because in the API when we create a product with a picture, we get download URL instead of the raw image.

### Navigation
Using fragments instead of activities might be a little tougher when it comes to launching other fragments. What we did was used ‘NavController’. First what we had to do is in the ‘mobile_navigation’ XML file we had to create a fragment reference. It had to contain navigation name, title and layout’s id. Then all we had to do was tell Navigation to navigate to our desired fragment, it will be placed on host fragment - MainActivity. We are also able to put some data inside the bundle. In our application, when we open the product’s details, we do exactly that. We create a bundle and put serializable product into it. For us to be able to do this, we also need Product class to be serializable, thus we simply tell Product to implement serializable.

![Navigation snippet](https://user-images.githubusercontent.com/32477822/161393879-954d8ef2-4225-49ae-a5b6-d4678ec3349a.png)

### Pagination
For pagination in android application, we used onScrolled listener. This listener listens to scroll events on the list. Here we get a visible amount of items, we also get total items and first visible products. We use these values to find out if items reached page size. If it did we also check if we aren’t currently loading items and if returned list of items isn’t equal to 0, this would indicate that we loaded the last page. 

![Pagination snippet](https://user-images.githubusercontent.com/32477822/161393900-f04cf1af-ebd4-45e1-be17-2384dbd3692b.png)
