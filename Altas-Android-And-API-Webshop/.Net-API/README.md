# Altas .Net API


### Architecture
We approached our .Net Core Application with Clean (Onion View) Architecture with it, we were able to control coupling to the fullest. The main rule is that the code in the center can depend on the layers that are closer to the center, but center layers cannot depend on the outer layers. This kind of architecture is only used for Object-oriented programming, that’s the reason why we have objects along with interfaces that are in the very center of the layers. An example is shown below

![API's architecture](https://i.imgur.com/ZUnFhiR.png)

### Testing
Used .Net Test SDK and Moq for testing. Moq was essential for unit testing. It allowed us to mock classes, instead of allowing them to act normally, and for example, getting data from the database. The best example of that would be tests for application services. Here we had to test logic. 

![Code coverage](https://user-images.githubusercontent.com/32477822/161393486-3675073e-e284-4135-aebf-fa81927add91.png)



### Searching
Searching is not a difficult task in Firebase. We are able to query the exact value in firebase, for example, we can search for ‘wheel’ and we will be able to return items that have exact value ‘wheel’. But if the user is not sure how the item is called or item has more than one word in it we will not be able to find this item. It’s very not user-friendly to not return the same item if it also contains clarification next to the searched word, for example, ‘wheel size 10’. What we came up with is more filtering than searching. When user types for example ‘Wheels’ we would return all items that start with the word ‘Wheels’. This provides a much better user experience.

![Searching snippet](https://user-images.githubusercontent.com/32477822/161394235-3be87b6c-6074-47f6-8f9a-25c1e62f19c0.png)

### Pagination/Infinate Scroll
Pagination in firebase is not that simple. Firebase does not have self incrementing ids, meaning ids are random, thus sorting items is redundant. The way we solved this issue is quite simple, yet effective. What we do is, first of all, we create the query, where sort items by either price, date, or brand. This leaves us with sorted products that will always be sorted in this way. Then we tell query from what item we should start taking other items. This item is ‘lastPageItem’, To get this item from the client, we simply take the last item in the list that contains these items. Finally, we limit the number of items we want to get from the database. This limit is the number of products we can fit on a page.

![Pagination snippet](https://user-images.githubusercontent.com/32477822/161394198-ff08a9f0-ce9e-4ef0-b9a2-e05dd0970467.png)
