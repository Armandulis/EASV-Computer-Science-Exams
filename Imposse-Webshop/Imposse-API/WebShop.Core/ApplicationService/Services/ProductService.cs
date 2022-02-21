using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;
using System.IO;

namespace WebShop.Core.ApplicationService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository productRepository)
        {
            _repo = productRepository;
        }

        public Product CreateProduct(Product product)
        {
            if (product.Name == null || product.Description == null || product.Price <= 0)
            {
                throw new InvalidDataException("A product must have a name, description and price.");
            }
            return _repo.CreateProduct(product);
        }

        public Product DeleteProduct(int id)
        {
            var productToDelete = _repo.DeleteProduct(id);
            if (productToDelete == null || id <= 0)
            {
                throw new Exception("No such product in the database");
            }
            return productToDelete;
        }

        public List<Product> GetAllProducts(Filter filter)
        {
            return _repo.GetAllProducts(filter).ToList();
        }

        public Product GetProductById(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null || id <= 0)
            {
                throw new Exception("No such product in the database");
            }
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            return _repo.UpdateProduct(product);
        }
    }
}
