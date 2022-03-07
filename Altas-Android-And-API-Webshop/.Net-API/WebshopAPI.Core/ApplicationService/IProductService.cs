using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.ApplicationService
{
    public interface IProductService
    {

        // CRUD
        // Create

        /// <summary>
        /// Valides given product values and calls repo to create product in the database
        /// </summary>
        /// <param name="product">Product with values that will be created</param>
        /// <returns>Product that was just created with ID</returns>
        Product Create(Product product);

        // Read 

        /// <summary>
        /// Calls Repository to get a product with provided id
        /// </summary>
        /// <param name="id">Product's id</param>
        /// <returns>Returns a spesific product with provided id</returns>
        Product GetById(string id);

        /// <summary>
        /// Checks if filter has SearchWord or LastItemId, will call repository based on set values
        /// </summary>
        /// <param name="filter">Will be used for searching/pagination/sorting</param>
        /// <returns>A list of products</returns>
        List<Product> GetProducts(Filter filter);

        // Update

        /// <summary>
        /// Check if Values are correct, call repo if they are, throw exception if not
        /// </summary>
        /// <param name="id">Will be used for a product that needs to be updated</param>
        /// <param name="product">product with new values</param>
        /// <returns>Updated product</returns>
        Product Update(string id, Product product);

        // Delete

        /// <summary>
        /// Check if value is currect and call repo
        /// </summary>
        /// <param name="productId">product's id that will be deleted</param>
        /// <returns>true if task was successful</returns>
        bool Delete(string productId);
    }
}
