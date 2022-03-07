using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebshopAPI.Core.DomainService;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.ApplicationService.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        /** BasketService constructor with DI */
        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        /** Validates provided values and calls repo to get products from the basket */
        public List<Product> GetBasketById(string basketId)
        {
            // Validate provided basket's id
            if (basketId.Equals("") || basketId == null)
            {
                // Throw exception if either basketId or productId is missing
                throw new MissingFieldException("BasketId must be set");
            }
            // Get products
            return _basketRepository.GetBasketByIdAsync(basketId).Result.ToList();
        }

        /** Validates provided values and calls repo to add product to provided basket */
        public bool AddProductToBasket(string basketId, string productId)
        {
            // Validate provided ids
            if (basketId.Equals("") || basketId == null ||
                productId.Equals("") || productId == null)
            {
                // Throw exception if either basketId or productId is missing
                throw new MissingFieldException("ProductId and basketId must be set");
            }

            // Adds product to basket
            return _basketRepository.AddProductToBasket(basketId, productId).Result;
        }

        /** Validates provided values and calls repo to remove provided product from basket */
        public bool RemoveProductFromBasket(string basketId, string productId)
        {
            // Validate provided ids
            if (basketId.Equals("") || basketId == null ||
              productId.Equals("") || productId == null)
            {
                // Throw exception if either basketId or productId is missing
                throw new MissingFieldException("ProductId and basketId must be set");
            }

            // Removes product from basket
            return _basketRepository.RemoveProductFromBasket(basketId, productId).Result;
        }
    }
}
