using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Core.DomainService;
using WebshopAPI.Core.Entity;
using Google.Cloud.Firestore;
using System.Threading.Tasks;
using System.IO;
using Firebase.Storage;

namespace WebshopAPI.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        // Firebase db values
        private static readonly string DB_PRODUCT_COLLECTION = "Products";
        private static readonly string DB_PRODUCT_BASKET_IMAGES = "ProductPictures";

        static readonly string baseDir = new DirectoryInfo(Environment.CurrentDirectory).FullName;
        static readonly string path = @"\AltasWebShop-76b5a69a5de4.json";
        static readonly string fullPath = baseDir + path;
        private readonly string credential_path = fullPath;

        private static readonly string ALTAS_FIREBASE_URL = "altaswebshop.appspot.com";
        private static readonly string ALTAS_FIRESTORE_ID = "altaswebshop";
        private readonly FirestoreDb firestoreDb;

        /** ProductRepository constructor */
        public ProductRepository()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            firestoreDb = FirestoreDb.Create(ALTAS_FIRESTORE_ID);
        }

        /** CREATE **/
        /** Saves product to database and get generated id */
        public async Task<Product> Create(Product product)
        {
            // Get Collection reference
            DocumentReference addedDocRef = firestoreDb.Collection(DB_PRODUCT_COLLECTION).Document();

            product.PictureUrl = SaveImageWithDownloadUlr(product.Picture, addedDocRef.Id).Result;
            product.Picture = "";

            // Set up data product in database
            Dictionary<string, object> productDictionary = new Dictionary<string, object>
            {
                { "Title", product.Title },
                { "Description", product.Description},
                { "Brand", product.Brand },
                { "Type", product.Type },
                { "Price", product.Price },
                { "PictureUrl", product.PictureUrl },
                { "Amount", product.Amount },

            };

            try
            {
                // Try to add product
                WriteResult addedProductRef = await addedDocRef.CreateAsync(productDictionary);
                // Set id to product
                product.Id = addedDocRef.Id;
            }
            catch (Exception e)
            {
                // NO-OP
            }

            // Return product with id
            return product;
        }

        /** READ **/

        /** Executes query to find products and extracts data from query **/
        public async Task<IEnumerable<Product>> GetProductsAsync(Filter filter)
        {
            List<Product> products = new List<Product>();

            // Get products collection reference
            CollectionReference productCollectionRef = firestoreDb.Collection(DB_PRODUCT_COLLECTION);

            // Get document snapshot that is last product of the page
            DocumentReference lastProductPageRef = productCollectionRef.Document(filter.LastItemId);
            DocumentSnapshot lastProductSnapshot = await lastProductPageRef.GetSnapshotAsync();

            // Query products, sort based on the value in filter and 
            Query query = productCollectionRef
                .OrderBy(filter.OrderBy)
                // get all documents after last product, limit for pagination
                .StartAfter(lastProductSnapshot)
                .Limit(10);

            // Execute query
            QuerySnapshot paginatedProducts = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in paginatedProducts.Documents)
            {
                // Try to extract data from documents to product
                Product product = ExtractProductFromSnapshot(documentSnapshot);
                products.Add(product);
            }

            // Return paginated products
            return products;
        }

        /** Executes query to find products and extracts data from query **/
        public async Task<IEnumerable<Product>> GetProductsFirstPageAsync(string orderBy)
        {
            List<Product> products = new List<Product>();

            // Get a reference to Products collection and order them, limit for pagination
            CollectionReference productCollectionRef = firestoreDb.Collection(DB_PRODUCT_COLLECTION);
            Query query = productCollectionRef.OrderBy(orderBy).Limit(10);

            // Execute query and get all documents
            QuerySnapshot productFirstPageSnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in productFirstPageSnapshot.Documents)
            {
                // Try to extract data from document and place it into product
                Product product = ExtractProductFromSnapshot(documentSnapshot);
                products.Add(product);
            }

            // Return a list of products
            return products;
        }


        /** Executes query to find products and extracts data from query **/
        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchWord)
        {
            List<Product> products = new List<Product>();

            // Get Products collection reference
            CollectionReference productsCollectionRef = firestoreDb.Collection(DB_PRODUCT_COLLECTION);

            // This is a work around for firestore to have query "LIKE"
            Query firstQuery = productsCollectionRef
                // Order ref by title and start with searchword 
                .OrderBy("Title").StartAt(searchWord).EndAt(searchWord + '\uf8ff')
                // Limit results to 25 to not overload data that we are passing to the user 
                .Limit(25);
            // This takes away possibility to order products in some way

            // Execute query
            QuerySnapshot allProductsQuerySnapshot = await firstQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot productSnapshot in allProductsQuerySnapshot.Documents)
            {
                // Try to extract data from document and put it in product
                Product product = ExtractProductFromSnapshot(productSnapshot);
                products.Add(product);
            }

            // Return products
            return products;
        }

        /** Executes query to find products and extracts data from query **/
        public async Task<Product> GetById(string id)
        {
            // Get document reference with given id from products collection
            DocumentReference productDocumentRef = firestoreDb.Collection(DB_PRODUCT_COLLECTION).Document(id);

            // Execute query and try to extract data from snapshot
            DocumentSnapshot productSnapshot = await productDocumentRef.GetSnapshotAsync();

            Product product = ExtractProductFromSnapshot(productSnapshot);

            // Return product with given id
            return product;
        }

        /** Gets limited amount of products from firebase and returns them */
        public async Task<IEnumerable<Product>> GetAmountOfProduct(int amount)
        {
            List<Product> products = new List<Product>();

            // Get Products collection reference
            CollectionReference productsCollectionRef = firestoreDb.Collection(DB_PRODUCT_COLLECTION);

            // Create query with limit of the amount
            Query firstQuery = productsCollectionRef.Limit(amount);

            // Execute query
            QuerySnapshot allProductsQuerySnapshot = await firstQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot productSnapshot in allProductsQuerySnapshot.Documents)
            {
                // Try to extract data from document and put it in products list
                Product product = ExtractProductFromSnapshot(productSnapshot);
                products.Add(product);
            }

            // Return products
            return products;
        }

        /** UPDATE **/

        /** Finds product by id and updates it with new provided values to the firebase */
        public async Task<Product> Update(string id, Product productUpdate)
        {
            // Get document reference with given id from products collection
            DocumentReference productDocumentRef = firestoreDb.Collection(DB_PRODUCT_COLLECTION).Document(id);

            // Set up new data product in database
            Dictionary<string, object> productUpdateDictionary = new Dictionary<string, object>
            {
                { "Title", productUpdate.Title },
                { "Description", productUpdate.Description},
                { "Brand", productUpdate.Brand },
                { "Type", productUpdate.Type },
                { "Price", productUpdate.Price },
                { "PictureUrl", productUpdate.PictureUrl },
                { "Amount", productUpdate.Amount },
            };

            try
            {
                // Update product
                await productDocumentRef.UpdateAsync(productUpdateDictionary);
            }
            catch (Exception e)
            {
                // Return updated product
                return productUpdate;
            }

            // Return updataded product
            return productUpdate;
        }

        /** DELETE **/

        /** Checks if product exists and deletes it from firestore */
        public async Task<bool> Delete(string productId)
        {
            // Get product's snapshot
            var docRef = firestoreDb.Collection(DB_PRODUCT_COLLECTION).Document(productId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // If it exists
            if (snapshot.Exists)
            {
                try
                {
                    RemoveProductPicture(productId);
                    // Delete document
                    await docRef.DeleteAsync();
                }
                catch (Exception e)
                {
                    // Return false because task was not successful
                    return false;
                }
            }

            // Return true because task was successful
            return true;
        }

        /** Converts base64 to file and uploads it to firebase storage, returns image's download ulr */
        public async Task<string> SaveImageWithDownloadUlr(String imageBase64, string productId)
        {
            // Convert string to stream
            byte[] bytes = Convert.FromBase64String(imageBase64);
            MemoryStream ms = new MemoryStream(bytes);

            // Set a place in Storage where we want to save file and save it with product's id
            var task = new FirebaseStorage(ALTAS_FIREBASE_URL)
                .Child(DB_PRODUCT_BASKET_IMAGES).Child(productId)
                .PutAsync(ms);

            // await the task to wait until upload completes and get the download url
            var downloadUrl = await task;


            return downloadUrl;
        }

        /** Removes a picture that has product's id */
        public async void RemoveProductPicture(string productId)
        {
            // Removes file from firebaseStorage
            await new FirebaseStorage(ALTAS_FIREBASE_URL)
                .Child(DB_PRODUCT_BASKET_IMAGES).Child(productId)
                .DeleteAsync();
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
