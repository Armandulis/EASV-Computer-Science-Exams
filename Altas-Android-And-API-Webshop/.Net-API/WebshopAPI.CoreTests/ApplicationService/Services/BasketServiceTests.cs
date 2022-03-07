using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebshopAPI.Core.ApplicationService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebshopAPI.Core.Entity;
using WebshopAPI.Core.DomainService;
using Moq;
using System.Linq;

namespace WebshopAPI.Core.ApplicationService.Services.Tests
{
    [TestClass()]
    public class BasketServiceTests
    {
        private Mock<IBasketRepository> mockedBasketRepo;
        private BasketService basketService;

        [TestInitialize]
        public void SetUp()
        {
            // Given BasketRepository and BasketService
            mockedBasketRepo = new Mock<IBasketRepository>();
            basketService = new BasketService(mockedBasketRepo.Object);
        }

        [TestMethod()]
        public void GetBasketByIdTest()
        {
            // Given Basket's id
            var id = "BaksetsId";

            // Given Task with products
            Task<IEnumerable<Product>> taskGetBasketProducts = Task.FromResult(CreateAmountOfProducts(1));

            mockedBasketRepo.Setup(repo => repo.GetBasketByIdAsync(
                // When called with basket's id
                It.Is<string>(basketId => basketId == id)
                )).Returns(taskGetBasketProducts);

            // When we call method GetBasketById with Basket's Id
            var response = basketService.GetBasketById(id);

            // Then we expect response to be a list of products  
            var expected = taskGetBasketProducts.Result.ToList();
            Assert.IsTrue(expected.SequenceEqual(response));
        }

        [TestMethod()]
        public void GetBasketByIdShouldThrowExceptionIfIdIsEmptyTest()
        {
            // When we call GetBasketById with empty id value
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => basketService.GetBasketById(""));

            // Then we expect exception to contain this message
            Assert.AreEqual("BasketId must be set", ex.Message);
        }


        [TestMethod()]
        public void AddProductToBasketTest()
        {
            // Given Basket's and product's id
            var basketId = "BaksetsId";
            var productid = "ProductsId";

            // Given Task with true value
            Task<bool> taskGetBasketProducts = Task.FromResult(true);

            mockedBasketRepo.Setup(repo => repo.AddProductToBasket(
                // When called with basket's and product's id
                It.Is<string>(baskId => baskId == basketId),
                It.Is<string>(prodId => prodId == productid)
                )).Returns(taskGetBasketProducts);

            // When we call method AddProductToBasket with Basket's and Product's Id
            var response = basketService.AddProductToBasket(basketId, productid);

            // Then we expect response to be true
            Assert.IsTrue(response);
        }

        [TestMethod()]
        [DataRow("", "productId", DisplayName = "Missing basketId")]
        [DataRow("basketId", "", DisplayName = "Missing productId")]
        public void AddProductToBasketShouldThrowExceptionIfProductOrBasketIdIsEmptyTest(string basketId, string productId)
        {
            // When we call AddProductToBasket with empty id values
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => basketService.AddProductToBasket(basketId, productId));

            // Then we expect exception to contain this message
            Assert.AreEqual("ProductId and basketId must be set", ex.Message);
        }


        [TestMethod()]
        public void RemoveProductFromBasketTest()
        {
            // Given Basket's and product's id
            var basketId = "BaksetsId";
            var productid = "ProductsId";

            // Given Task with true value
            Task<bool> taskGetBasketProducts = Task.FromResult(true);

            mockedBasketRepo.Setup(repo => repo.RemoveProductFromBasket(
                // When called with basket's and product's id
                It.Is<string>(baskId => baskId == basketId),
                It.Is<string>(prodId => prodId == productid)
                )).Returns(taskGetBasketProducts);

            // When we call method RemoveProductFromBasket with Basket's and Product's Id
            var response = basketService.RemoveProductFromBasket(basketId, productid);

            // Then we expect response to be true
            Assert.IsTrue(response);
        }

        [TestMethod()]
        [DataRow("", "productId", DisplayName = "Missing basketId")]
        [DataRow("basketId", "", DisplayName = "Missing productId")]
        public void RemoveProductFromBasketShouldThrowExceptionIfProductOrBasketIdIsEmptyTest(string basketId, string productId)
        {
            // When we call RemoveProductFromBasket with empty id values
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => basketService.RemoveProductFromBasket(basketId, productId));

            // Then we expect exception to contain this message
            Assert.AreEqual("ProductId and basketId must be set", ex.Message);
        }

        /// <summary>
        /// Creates a wanted amount of products, puts them in a list and returns it
        /// </summary>
        /// <param name="amount">Wanted amount of products in the list</param>
        /// <returns>List of products</returns>
        private IEnumerable<Product> CreateAmountOfProducts(int amount)
        {
            List<Product> productList = new List<Product>();

            for (int i = 0; i < amount; i++)
            {
                Product product = new Product()
                {
                    Id = i + "WQ",
                    Title = "Title"
                };
                productList.Add(product);
            }

            return productList;
        }
    }
}