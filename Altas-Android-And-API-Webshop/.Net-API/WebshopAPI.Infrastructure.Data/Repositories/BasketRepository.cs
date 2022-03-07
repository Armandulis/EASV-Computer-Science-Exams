using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebshopAPI.Core.DomainService;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Infrastructure.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        // Firestore values
        private static readonly string ALTAS_FIRESTORE_ID = "altaswebshop";
        private static readonly string DB_BASKET_COLLECTION = "Baskets";
        private static readonly string DB_PRODUCT_COLLECTION = "Products";

        static readonly string baseDir = new DirectoryInfo(Environment.CurrentDirectory).FullName;
        static readonly string path = @"\AltasWebShop-76b5a69a5de4.json";
        static readonly string fullPath = baseDir + path;
        private readonly string credential_path = fullPath;

        public string ALTAS_FIREBASE_URL = "altaswebshop.appspot.com";

        private readonly FirestoreDb firestoreDb;

        /** BasketRepository constructor **/
        public BasketRepository()
        {
            // Set up database
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);

            firestoreDb = FirestoreDb.Create(ALTAS_FIRESTORE_ID);
        }


        /** Validates if basket needs to be created and adds product's id to it */
        public async Task<bool> AddProductToBasket(string basketId, string productId)
        {
            // List that holds products ids that are in the basket
            List<string> productsIds = new List<string>();

            // Add product to list
            productsIds.Add(productId);

            // Get Basket
            var docRef = firestoreDb.Collection(DB_BASKET_COLLECTION).Document(basketId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // If it exists
            if (snapshot.Exists)
            {
                // Get basket's products list
                productsIds.AddRange(snapshot.GetValue<List<string>>("ProductIds"));
                // Set new value for basket's products list
                Dictionary<string, object> basket = new Dictionary<string, object>
                {
                    { "ProductIds", productsIds }
                };
                try
                {
                    // Update Basket
                    await docRef.UpdateAsync(basket);
                }
                catch (Exception e)
                {
                    // If  error occoured return false
                    return false;
                }


            }
            else
            {
                // If basket doesnt exist
                // Create basket with productsIds
                Dictionary<string, object> basket = new Dictionary<string, object>
                {
                    { "ProductIds", productsIds }
                };

                // Get Reference to basket with provided id
                DocumentReference addedDocRef = firestoreDb.Collection(DB_BASKET_COLLECTION).Document(basketId);
                try
                {
                    // Create basket in database
                    WriteResult wr = await addedDocRef.CreateAsync(basket);
                }
                catch (Exception e)
                {
                    // If  error occoured return false
                    return false;
                }
            }

            // Task was successfull return true
            return true;
        }

        /** Gets products from the basket */
        public async Task<IEnumerable<Product>> GetBasketByIdAsync(string basketId)
        {

            List<Product> products = new List<Product>();

            // Get basket's snapshot
            var docRef = firestoreDb.Collection(DB_BASKET_COLLECTION).Document(basketId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // Get a list of product ids
            List<string> productsIds = snapshot.GetValue<List<string>>("ProductIds");

            // Save removedProductIds
            List<string> removedProductIds = new List<string>();
            foreach (string productId in productsIds)
            {
                // Get product's reference
                var productDoc = firestoreDb.Collection(DB_PRODUCT_COLLECTION).Document(productId);

                DocumentSnapshot productSnapshot = await productDoc.GetSnapshotAsync();

                if (productSnapshot.Exists)
                {
                    // Get product's values
                    Product product = ExtractProductFromSnapshot(productSnapshot);

                    // Add product to list
                    products.Add(product);
                }
                else
                {
                    // If product's document no longer exists add it to removedProductIds list
                    removedProductIds.Add(productId);
                }
            }

            try
            {
                // Remove no longer existing product ids from basket after getting them to avoid getting same products
                foreach (string removedProductId in removedProductIds)
                {
                    // Remove products from basket
                    await RemoveProductFromBasket(basketId, removedProductId);
                }
            }
            catch
            {
                // NO-OP
            }


            // Return products
            return products;
        }


        /** Get basket's products and remove provided product, then save it */
        public async Task<bool> RemoveProductFromBasket(string basketId, string productId)
        {
            // Get basket's snapshot
            var docRef = firestoreDb.Collection(DB_BASKET_COLLECTION).Document(basketId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // If it exists
            if (snapshot.Exists)
            {
                // Get basket's products list
                List<string> productsIds = snapshot.GetValue<List<string>>("ProductIds");
                // Remove product from the list 
                productsIds.Remove(productId);
                Dictionary<string, object> basket = new Dictionary<string, object>
                {
                    { "ProductIds", productsIds }
                };

                try
                {
                    // Update Basket
                    await docRef.UpdateAsync(basket);
                }
                catch (Exception e)
                {
                    // Return false because task was not successfull
                    return false;
                }
            }

            // Return true because task was successfull
            return true;
        }

        /// <summary>
        /// Extracts data from snapshot and puts it in to product
        /// </summary>
        /// <param name="productSnapshot">Snapshot from firebase</param>
        /// <returns>product with set values</returns>
        public Product ExtractProductFromSnapshot(DocumentSnapshot productSnapshot)
        {
            Product product = new Product();
            try
            {
                // Get values from snapshot
                product.Id = productSnapshot.Id;
                product.Title = productSnapshot.GetValue<string>("Title");
                product.Price = productSnapshot.GetValue<string>("Price");
                product.Type = productSnapshot.GetValue<string>("Type");
                product.Brand = productSnapshot.GetValue<string>("Brand");
                product.Description = productSnapshot.GetValue<string>("Description");
                product.PictureUrl = productSnapshot.GetValue<string>("PictureUrl");
                product.Amount = productSnapshot.GetValue<string>("Amount");
            }
            catch (Exception e)
            {
                // NO-OP
            }

            // Return product
            return product;
        }
    }
}
