# Imposse webshop API - .Net C#
- More in-depth documentation can be found in this document: https://docs.google.com/document/d/1RDeL3qAPaae55dwr8ufjZ5oBm3TUR508QtSohuu4y5o

### .Net Architecture
The API was made with .Net C#. Onion architecture was used - it provides better maintainability as all the codes depend on layers or the program’s center. It 
consists of `Domain Entities Layer`, `Repository Layer`, `Service Layer`, and `UI/Controller Layer`.

###  Conceptual Data Model
A product can have 0 to many reviews and be in 0 to many baskets. A review is of one product and is by one user. A user has one basket and can have 0 to many reviews and stories. A basket has one user and can have 0 to many products in it. Lastly a story has on user.
Using the Entity Framework Fluent Api we set up the table relations and delete behaviour.

![Demo Conceptual Data Model ](https://i.imgur.com/Y8gYjIW.png)

Here we set the delete behaviour to cascade which means that when an entity gets deleted, all entities that have a foreign key pointing to that entity will also be deleted. This is so that if a user or product gets deleted all their associated reviews, stories etc. get deleted as well and not left behind with empty reference fields, cluttering up the database or causing errors in the web client.

![Demo Code](https://i.imgur.com/Lu22Lge.png)
