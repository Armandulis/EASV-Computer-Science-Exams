using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.DomainService
{
    public interface IProductRepository
    {
        /// <summary>
        /// Get all products from the database,
        /// with an optional filter for paging purposes.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<Product> GetAllProducts(Filter filter = null);

        /// <summary>
        /// Get a product from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProductById(int id);

        /// <summary>
        /// Add a new product to the database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product CreateProduct(Product product);

        /// <summary>
        /// Update a product in the database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product UpdateProduct(Product product);

        /// <summary>
        /// Delete a product from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product DeleteProduct(int id);
    }
}
