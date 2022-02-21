using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService
{
    public interface IBasketService
    {
        /// <summary>
        /// Adds the given basket to the database.
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        Basket NewBasket(Basket basket);

        /// <summary>
        /// Returns a basket from the database using the id of it's user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Basket GetBasket(int userId);

        /// <summary>
        /// Adds the given product to the basket with the given user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Basket AddToBasket(int userId, Product product);

        /// <summary>
        /// Empties the basket of products.
        /// </summary>
        /// <param name="userId"></param>
        void ClearBasket(int userId);

        /// <summary>
        /// Deletes the basket
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Basket DeleteBasket(int userId);

        /// <summary>
        /// Returns a list of products in the basket.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Product> GetProductsInBasket(int userId);

        /// <summary>
        /// Deletes the given product from the basket
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="product"></param>
        void DeleteFromBasket(int userId, Product product);
    }
}
