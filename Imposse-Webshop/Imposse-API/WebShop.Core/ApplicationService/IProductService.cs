using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService
{
    public interface IProductService
    {
        /// <summary>
        /// Return a list of all products.
        /// Filter is optional and will limit
        /// the number of products for paging purposes.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<Product> GetAllProducts(Filter filter = null);

        /// <summary>
        /// Return a single product using id.
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
        /// Update an existing product with new information.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product UpdateProduct(Product product);

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product DeleteProduct(int id);
    }
}
