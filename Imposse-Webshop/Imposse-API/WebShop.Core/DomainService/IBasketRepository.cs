using WebShop.Core.Entity;

namespace WebShop.Core.DomainService
{
    public interface IBasketRepository
    {
        /// <summary>
        /// Delete a basket from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Basket DeleteBasket(int id);

        /// <summary>
        /// Get a basket from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Basket GetBasket(int id);

        /// <summary>
        /// Add a new basket to the database.
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        Basket NewBasket(Basket basket);

        /// <summary>
        /// Updates a basket in the database.
        /// If product is not null, it gets added to the basket.
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="product"></param>
        void UpdateBasket(Basket basket, Product product = null);

        /// <summary>
        /// Get a basket from the database
        /// with its user and products included
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Basket GetBasketIncludeUserAndProducts(int id);

        /// <summary>
        /// Delete the product from the basket.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        void DeleteFromBasket(int id, Product product);
    }
}