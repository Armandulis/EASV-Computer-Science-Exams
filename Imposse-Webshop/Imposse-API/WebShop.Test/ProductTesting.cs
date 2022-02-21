using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebShop.Core.ApplicationService;
using WebShop.Core.ApplicationService.Services;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;
using Xunit;

namespace WebShop.Test
{
    public class ProductTesting
    {
        [Fact]
        public void TestProductCreation()
        {
            var prodRepo = new Mock<IProductRepository>();
            IProductService productService = new ProductService(prodRepo.Object);

            var product = new Product()
            {
                Name = "test",
                Price = 200
            };

            Exception ex = Assert.Throws<InvalidDataException>(() => productService.CreateProduct(product));
            Assert.Equal("A product must have a name, description and price.", ex.Message);
        }

        [Fact]
        public void TestProductDeletion()
        {
            var prodRepo = new Mock<IProductRepository>();
            IProductService productService = new ProductService(prodRepo.Object);

            Exception ex = Assert.Throws<Exception>(() => productService.DeleteProduct(55));
            Assert.Equal("No such product in the database", ex.Message);
        }

        [Fact]
        public void TestGetProductById()
        {
            var prodRepo = new Mock<IProductRepository>();
            IProductService productService = new ProductService(prodRepo.Object);

            Exception ex = Assert.Throws<Exception>(() => productService.GetProductById(0));
            Assert.Equal("No such product in the database", ex.Message);
        }
    }
}
