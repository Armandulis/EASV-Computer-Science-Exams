using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.DomainService
{
    public interface IProductRepository
    {
        /** Create **/

        /// <summary>
        /// Creates a product in the database and sets generated id to the product 
        /// </summary>
        /// <param name="product">Product to save in database</param>
        /// <returns>Product with generated id set</returns>
        Task<Product> Create(Product product);

        /** Read **/

        /// <summary>
        /// Get Product with provided product's id
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <returns>Product with provided id</returns>
        Task<Product> GetById(string id);

        /// <summary>
        /// Gets a reference to a limited amount of products and returns them from firebase
        /// </summary>
        /// <param name="amount">Number of wanted products</param>
        /// <returns>List of limited amount of products</returns>
        Task<IEnumerable<Product>> GetAmountOfProduct(int amount);

        /// <summary>
        /// Gets paginated products that are not on the first page
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Paginated products</returns>
        Task<IEnumerable<Product>> GetProductsAsync(Filter filter);

        /// <summary>
        /// Gets First page of products from Cloud Firestore
        /// </summary>
        /// <param name="orderBy">Used to sort products</param>
        /// <returns>IEnumerable of products from first page </returns>
        Task<IEnumerable<Product>> GetProductsFirstPageAsync(string orderBy);

        /// <summary>
        /// Search Cloud Firestore for a product that beggin searchword in the title
        /// </summary>
        /// <param name="searchWord">search word for product</param>
        /// <returns>IEnumerable of Products that beggin with search word in the title</returns>
        Task<IEnumerable<Product>> SearchProductsAsync(string searchWord);



        /** Update **/

        /// <summary>
        /// Find a Product by id and updates it
        /// </summary>
        /// <param name="id"> Id of the Product</param>
        /// <param name="productUpdate"> New product's values</param>
        /// <returns> Updated Product</returns>
        Task<Product> Update(string id, Product product);

        /** Delete **/

        /// <summary>
        /// Check if product exists and deletes it from database
        /// </summary>
        /// <param name="productId">Product's id</param>
        /// <returns>True if task was successful</returns>
        Task<bool> Delete(string productId);


        /// <summary>
        /// Converts string to stream and uploads it to storage with given product's id and returns download ulr
        /// </summary>
        /// <param name="imageBase64">Image converted to base64 string</param>
        /// <param name="productId">product's id will be as pictures</param>
        /// <returns>Download ulr for image</returns>
        void RemoveProductPicture(string productId);

        /// <summary>
        /// Removes file from directory ProductPictures with provided id
        /// </summary>
        /// <param name="productId">product's picture</param>
        Task<string> SaveImageWithDownloadUlr(String imageBase64, string productId);

    }
}
