using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebshopAPI.Core.DomainService;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.ApplicationService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        /** ProductService constructor with DI */
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /** Validates provided values and calls Repo to create product with provided values*/
        public Product Create(Product product)
        {
            // Validate product's core values
            if (product.Brand == null || product.Brand.Equals("") ||
                product.Price == null || product.Price.Equals("") ||
                product.Title == null || product.Title.Equals("") ||
                product.Type == null || product.Type.Equals("") ||
                product.Picture == null || product.Picture.Equals(""))
            {
                // Throw exception if some core values are missing
                throw new MissingFieldException("Product has to have brand, price, title, type and picture set");
            }

            // Calls repo
            return _productRepository.Create(product).Result;

        }

        /**  Validates provided values and call repository based on set values of filter **/
        public List<Product> GetProducts(Filter filter)
        {
            if (filter.Amount != 0)
            {
                return _productRepository.GetAmountOfProduct(filter.Amount).Result.ToList();
            }
            // Check if filter contains SearchWord
            if (filter.SearchWord != null)
            {
                // If it does, search for product
                return _productRepository.SearchProductsAsync(filter.SearchWord).Result.ToList();
            }

            // Check if filter has LastItemId
            if (filter.LastItemId != null)
            {
                // If it does it means that it is not the first page of products
                return _productRepository.GetProductsAsync(filter).Result.ToList();
            }

            if (filter.OrderBy != null)
            {
                // Order by is always needed to return a first page of products
                return _productRepository.GetProductsFirstPageAsync(filter.OrderBy).Result.ToList();
            }

            // Throw exception if filter doesn't have any of the values set
            throw new MissingFieldException("Filter must have some value");
        }

        /** Validates provided values and calls repository to get product based on id **/
        public Product GetById(string id)
        {
            // Check if id is not empty
            if (id.Equals(""))
            {
                // Throw exception if id is empty
                throw new MissingFieldException("Product's id cannot be empty");
            }

            // Calls repository to get a spesific product
            return _productRepository.GetById(id).Result;
        }

        /** Validates provided values and calls repository to update product */
        public Product Update(string id, Product product)
        {
            // Validate provided id
            if (!id.Equals(product.Id) || id.Equals(""))
            {
                // Throw exception if id is empty or doesn't match product's id
                throw new MissingFieldException("Provided id and Product's id must match and can't be empty");
            }

            // Validate product's core values
            if (product.Brand == null || product.Brand.Equals("") ||
                product.Price == null || product.Price.Equals("") ||
                product.Title == null || product.Title.Equals("") ||
                product.Type == null || product.Type.Equals("") ||
                product.PictureUrl == null || product.PictureUrl.Equals(""))
            {
                // Throw exception if some core values are missing
                throw new MissingFieldException("Product has to have brand, price, title, type and pictureUrl set");
            }

            // Call repo
            return _productRepository.Update(id, product).Result;
        }

        /** Validates provided values and calls repository to remove product */
        public bool Delete(string id)
        {
            // Check if id has value
            if (id.Equals(""))
            {
                // Throw exception if id is empty
                throw new MissingFieldException("Product's id has to have value");
            }

            // Calls repo to remove product
            return _productRepository.Delete(id).Result;
        }
    }
}
