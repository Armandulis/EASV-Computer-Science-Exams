using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repo;

        public BasketService(IBasketRepository basketRepository)
        {
            _repo = basketRepository;
        }

        public Basket AddToBasket(int id, Product product)
        {
            var basket = GetBasket(id);
            _repo.UpdateBasket(basket, product);
            return basket;

        }

        public void ClearBasket(int id)
        {
            var basket = GetBasket(id);
            basket.Products.Clear();
            _repo.UpdateBasket(basket);

        }

        public Basket DeleteBasket(int id)
        {
            return _repo.DeleteBasket(id);
        }

        public void DeleteFromBasket(int id, Product product)
        {
            _repo.DeleteFromBasket(id, product);
        }

        public Basket GetBasket(int id)
        {
            return _repo.GetBasketIncludeUserAndProducts(id);
        }

        public List<Product> GetProductsInBasket(int basketId)
        {
            return GetBasket(basketId).Products;
        }

        public Basket NewBasket(Basket basket)
        {
            return _repo.NewBasket(basket);
        }
    }
}
