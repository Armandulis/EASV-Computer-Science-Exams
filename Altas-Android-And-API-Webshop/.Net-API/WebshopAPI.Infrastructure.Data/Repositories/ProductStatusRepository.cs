using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebshopAPI.Core.DomainService;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Infrastructure.Data.Repositories
{
    public class ProductStatusRepository : IProductStatusRepository
    {
        // Firebase db values
        private static readonly string DB_STATUS_COLLECTION = "ProductStatus";
        private static readonly string DB_STATUS_USER_ID = "UserId";
        private static readonly string DB_STATUS_PRODUCT_ID = "ProductId";
        private static readonly string DB_STATUS_PRODUCT_PURCHASE_DATE = "PurchaseDate";
        private static readonly string DB_STATUS_PRODUCT_STATUS = "ProductStatus";

        static readonly string baseDir = new DirectoryInfo(Environment.CurrentDirectory).FullName;
        static readonly string path = @"\AltasWebShop-76b5a69a5de4.json";
        static readonly string fullPath = baseDir + path;
        private readonly string credential_path = fullPath;

        public string ALTAS_FIREBASE_URL = "altaswebshop.appspot.com";
        private readonly IProductRepository _productRepository;
        private readonly FirestoreDb firestoreDb;

        /** ProductStatusRepository constructor with DI */
        public ProductStatusRepository(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            // Set up database
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            firestoreDb = FirestoreDb.Create("altaswebshop");
        }

        /**Gets ProductStatus all product status that has user's id from the database and return list of ProductStatus */
        public async Task<IEnumerable<ProductStatus>> GetAllProductStatus(string userId)
        {
            List<ProductStatus> productStatusList = new List<ProductStatus>();

            // Get a reference to ProductStatus docuemnts that have user's id
            CollectionReference productStatusCollectionRef = firestoreDb.Collection(DB_STATUS_COLLECTION);
            Query query = productStatusCollectionRef.WhereEqualTo(DB_STATUS_USER_ID, userId);

            // Execute query and get all documents
            QuerySnapshot ProductStatusSnapshot = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in ProductStatusSnapshot.Documents)
            {
                // Try to extract data from document and place it into product
                ProductStatus productStatus = ExtractProductFromSnapshot(documentSnapshot);
                productStatusList.Add(productStatus);
            }

            // Return all User's product status
            return productStatusList;
        }

        /** Gets reference to productStatus document and removes it */
        public async Task<bool> RemoveProductStatus(string productStatusId)
        {
            // Get productStatus snapshot
            var docRef = firestoreDb.Collection(DB_STATUS_COLLECTION).Document(productStatusId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // If it exists
            if (snapshot.Exists)
            {
                try
                {
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

        /** Get productStatus document reference and update it */
        public async Task<bool> UpdateProductStatus(ProductStatus productStatus)
        {
            // Get productStatus snapshot
            DocumentReference productDocumentRef = firestoreDb.Collection(DB_STATUS_COLLECTION).Document(productStatus.Id);

            // Set up new status data for productStatus in database
            Dictionary<string, object> productUpdateDictionary = new Dictionary<string, object>
            {
                { DB_STATUS_USER_ID, productStatus.UserId },
                { DB_STATUS_PRODUCT_ID, productStatus.Product.Id},
                { DB_STATUS_PRODUCT_STATUS, productStatus.Status },
                {DB_STATUS_PRODUCT_PURCHASE_DATE, productStatus.PurchaseDate }
            };

            try
            {
                // Update productStatus
                await productDocumentRef.UpdateAsync(productUpdateDictionary);
            }
            catch (Exception e)
            {
                // Return false because task was not successful
                return false;
            }

            // Return true because task was successful
            return true;
        }



        /** Create a document for productStatus in database*/
        public async Task<bool> CreateProductStatus(ProductStatus productStatus)
        {
            // Get productStatus reference
            DocumentReference productDocumentRef = firestoreDb.Collection(DB_STATUS_COLLECTION).Document();


            // Set up data for productStatus in database
            Dictionary<string, object> productUpdateDictionary = new Dictionary<string, object>
            {
                { DB_STATUS_USER_ID, productStatus.UserId },
                { DB_STATUS_PRODUCT_ID, productStatus.Product.Id},
                { DB_STATUS_PRODUCT_STATUS, productStatus.Status },
                {DB_STATUS_PRODUCT_PURCHASE_DATE, productStatus.PurchaseDate }
            };

            try
            {
                // Update productStatus
                await productDocumentRef.CreateAsync(productUpdateDictionary);
            }
            catch (Exception e)
            {
                // Return false because task was not successful
                return false;
            }

            // Return true because task was successful
            return true;
        }

        /// <summary>
        /// Gets sorted documents by date, checks if we need to start pagination from page 1 or not, and returns limited
        /// amount of productStatus
        /// </summary>
        /// <param name="filter">filter with last productStatus of the page or empty</param>
        /// <returns>paginated list of prouctStatus</returns>
        public async Task<IEnumerable<ProductStatus>> GetAllPaginatedProductStatuses(Filter filter)
        {
            List<ProductStatus> productStatusList = new List<ProductStatus>();

            // Get a reference to ProductStatus docuemnts that have user's id
            CollectionReference productStatusCollectionRef = firestoreDb.Collection(DB_STATUS_COLLECTION);
            Query query = productStatusCollectionRef.OrderBy(DB_STATUS_PRODUCT_PURCHASE_DATE);

            // Check if we need first page or not
            if (filter.LastItemId != null && !filter.LastItemId.Equals(""))
            {
                query = query.StartAfter(filter.LastItemId);
            }

            // Execute query and get limited documents
            QuerySnapshot ProductStatusSnapshot = await query.Limit(25).GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in ProductStatusSnapshot.Documents)
            {
                // Try to extract data from document and place it into product
                ProductStatus productStatus = ExtractProductFromSnapshot(documentSnapshot);
                productStatusList.Add(productStatus);
            }

            // Return paginated product status
            return productStatusList;
        }

        /// <summary>
        /// Extracts values from documentSnapshot and places them into ProductStatus, also gets product by id
        /// </summary>
        /// <param name="documentSnapshot">Snapshot of ProductStatus</param>
        /// <returns>Product status with values</returns>
        private ProductStatus ExtractProductFromSnapshot(DocumentSnapshot documentSnapshot)
        {
            ProductStatus productStatus = new ProductStatus();
            try
            {
                // Get values from snapshot
                productStatus.Id = documentSnapshot.Id;
                productStatus.UserId = documentSnapshot.GetValue<string>(DB_STATUS_USER_ID);
                productStatus.Status = documentSnapshot.GetValue<string>(DB_STATUS_PRODUCT_STATUS);
                productStatus.PurchaseDate = documentSnapshot.GetValue<string>(DB_STATUS_PRODUCT_PURCHASE_DATE);

                // Get product 
                string productId = documentSnapshot.GetValue<string>(DB_STATUS_PRODUCT_ID);
                productStatus.Product = _productRepository.GetById(productId).Result;

            }
            catch (Exception e)
            {
                // NO-OP
            }

            // Return productStatus
            return productStatus;
        }

    }
}
