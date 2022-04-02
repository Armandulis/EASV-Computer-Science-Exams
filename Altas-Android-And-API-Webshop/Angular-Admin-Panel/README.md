# Altas Admin panel Angular client
### Service
The services are responsible for communicating with the rest API, this is where we write the crud functions that will call to the rest API to create, update, get or delete data. We use models that mirror the entities in the rest API for when we want to send or receive data from the rest API.

![Service snippet](https://user-images.githubusercontent.com/32477822/161393986-b9dd0afc-2750-4c08-9fc7-c815c01fd93e.png)


### Load more
nstead of using pages, we decided to go with a simple button to load more products or product statuses. This creates a controllable and simple solution. Pagination is done by getting the id of the last productstatus in the array of productstatuses, we then use that to get products from that id and onwards. once we get the data then we combine the old and new data into the productstatus array and this effectively means that we have an infinite scrollable list of productstatuses. 

![Load more snippet](https://user-images.githubusercontent.com/32477822/161394119-696e9a5c-43f3-4acd-9dab-519357826b4f.png)
