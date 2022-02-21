using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Infrastructure.Data
{
    public class DBSeed
    {
        public static void SeedDB(WebShopContext ctx)
        {
            // DELETES AND CREATES DATABASE
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();


            var password = "Password";
            // HASHES PASWORDS
            byte[] passwordHashA, passwordSaltA, passwordHashAdmin, passwordSaltAdmin;

            CreatePasswordHash(password, out passwordHashA, out passwordSaltA);
            CreatePasswordHash(password, out passwordHashAdmin, out passwordSaltAdmin);

            // CEATING USERS
            var user1 = new User()
            {
                Firstname = "Generic name",
                Lastname = "Generic lastname",
                Username = "Admin",
                PasswordHash = passwordHashAdmin,
                PasswordSalt = passwordSaltAdmin,
                IsAdmin = true,
                Picture = "https://img.thedailybeast.com/image/upload/c_crop,d_placeholder_euli9k,h_1440,w_2560,x_0,y_0/dpr_2.0/c_limit,w_740/fl_lossy,q_auto/v1531451526/180712-Weill--The-Creator-of-Pepe-hero_uionjj"

            };

            var user2 = new User()
            {
                Firstname = "lars",
                Lastname = "the web master",
                Username = "User",
                PasswordHash = passwordHashA,
                PasswordSalt = passwordSaltA,
                IsAdmin = true,
                Picture = "https://fsmedia.imgix.net/77/4f/7a/cf/6327/4cc2/a533/4fbb91a213ca/spongbob.jpeg?rect=0%2C47%2C750%2C375&dpr=2&auto=format%2Ccompress&w=650"

            };

            ctx.Users.Add(user1);
            ctx.Users.Add(user2);
            
            //CREATING STORIES
            ctx.Stories.Add(new Story()
            {
                Date = DateTime.Now.ToString("H:mm dd/MM/yyyy"),
                Title = "Pls fcking work",
                Text ="This is a Great Story",
                User = user1
            });
            ctx.Stories.Add(new Story()
            {
                Date = DateTime.Now.ToString("H:mm dd/MM/yyyy"),
                Title = "u did fcking work, right?",
                Text = "This is a Great jkwbdkaj wd",
                User = user1
            });
            ctx.Stories.Add(new Story()
            {
                Date = DateTime.Now.ToString("H:mm dd/MM/yyyy"),
                Title = "BBC NEWS STORY",
                Text = "TRUMP BEATS OBAMA IN ANIME CAT GIRL FIGHT",
                User = user2

            });

            //CREATING PRODUCTS
            var product1 = ctx.Products.Add(new Product()
            {
                Name = "shirt yas?",
                Picture = "https://i.imgur.com/dmvqXtL.jpg",
                Price = 25.99,
                Description = "Computer science is the theory, experimentation, and engineering that " +
                "form the basis for the design and use of computers. It involves the study of algorithms that" +
                " process, store, and communicate digital information. A computer scientist specializes in the theory " +
                "of computation and the design of computational systems.",
                Gender = "Male",
                Color = "White",
                Type = "Polo",



            }).Entity;

            var product2 = ctx.Products.Add(new Product()
            {
                Name = "a fcking shirt",
                Picture = "https://i.imgur.com/krCJM.jpg",
                Price = 2299,
                Description = "A shirt is a cloth garment for the upper body (from the neck to the waist). ..." +
                " In British English, a shirt is more specifically a garment with a collar, sleeves with cuffs, " +
                "and a full vertical opening with buttons or snaps (North Americans would call that a dress shirt, " +
                "a specific type of collared shirt).",
                Gender = "Male",
                Color = "Black",
                Type = "Shirt",
            }).Entity;

            var product3 = ctx.Products.Add(new Product()
            {
                Name = "Pepe Shirt",
                Picture = "https://i.imgur.com/tBTltNJ.jpg",
                Price = 2299,
                Description = "Pepe the Frog (/ˈpɛpeɪ/) is a popular Internet meme. A green anthropomorphic frog " +
                "with a humanoid body, Pepe originated in a comic by Matt Furie called Boy's Club. It became an " +
                "Internet meme when its popularity steadily grew across Myspace, Gaia Online and 4chan in 2008.",
                Gender = "Male",
                Color = "Black",
                Type = "Shirt",
            }).Entity;

            var product4 = ctx.Products.Add(new Product()
            {
                Name = "Nicolas Shirt",
                Picture = "https://i.kym-cdn.com/entries/icons/original/000/017/411/1424933369558.jpg",
                Price = 2299,
                Description = "Mocking SpongeBob, also known as Spongemock, refers to an image macro " +
                "featuring cartoon character SpongeBob SquarePants in which people use a picture of SpongeBob " +
                "to indicate a mocking tone towards an opinion or point of view.",
                Gender = "Male",
                Color = "Black",
                Type = "Shirt",
            }).Entity;

            //CREATING REVIEWS
            ctx.Reviews.Add(new Review()
            {
                Comment = "Great Product",
                Rating = 5,
                User = user1,
                Product = product1



            });

            ctx.Reviews.Add(new Review()
            {
                Comment = "fantastic Product",
                Rating = 5,
                User = user2,
                Product = product2
            });

            //HASHING PASSWORDS FOR USER METHODS


            void CreatePasswordHash(string passwordToHash, out byte[] passwordHash, out byte[] passwordSalt)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordToHash));
                }
            }

            ctx.SaveChanges();
        }

    }
}
