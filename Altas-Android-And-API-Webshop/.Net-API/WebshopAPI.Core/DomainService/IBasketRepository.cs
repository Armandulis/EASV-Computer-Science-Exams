using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.DomainService
{
    public interface IBasketRepository
    {
        /// <summary>
        /// Gets all products from the basket
        /// </summary>
        /// <param name="basketId">Basket's id</param>
        /// <returns>Products that are saved in the basket</returns>
        Task<IEnumerable<Product>> GetBasketByIdAsync(string basketId);

        /// <summary>
        /// Validates if basket needs to be created and adds product's id to it
        /// </summary>
        /// <param name="basketId">Basket's unique ID</param>
        /// <param name="productId">Product's id that will be added to basket</param>
        /// <returns>Return true if task was successful</returns>
        Task<bool> AddProductToBasket(string basketId, string productId);

        /// <summary>
        /// Gets basket products, removes provided product from basket and saves basket
        /// </summary>
        /// <param name="basketId">Basket's unique ID</param>
        /// <param name="productId">product's Id</param>
        /// <returns>Returns true if task was successfull</returns>
        Task<bool> RemoveProductFromBasket(string basketId, string productId);

    }
}
