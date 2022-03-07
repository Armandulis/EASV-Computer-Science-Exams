using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.ApplicationService
{
    public interface IBasketService
    {
        /// <summary>
        /// Validates given basket Id and calls repo to get products from the basket
        /// </summary>
        /// <param name="basketId">Basket's id</param>
        /// <returns>List of products that are in the basket</returns>
        List<Product> GetBasketById(string basketId);

        /// <summary>
        /// Validates given basket id and product id, calls repo to add product to the basket
        /// </summary>
        /// <param name="basketId">Basket's id</param>
        /// <param name="productId">Product's id that will be added to the basket</param>
        /// <returns>True if task was successful</returns>
        bool AddProductToBasket(string basketId, string productId);

        /// <summary>
        /// Validates given basket id and product id, calls repo to remove product from the basket
        /// </summary>
        /// <param name="basketId">Basket's id</param>
        /// <param name="productId">Product's id that will be removed from the basket</param>
        /// <returns>True if task was successful</returns>
        bool RemoveProductFromBasket(string basketId, string productId);
    }
}
