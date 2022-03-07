using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebshopAPI.Core.ApplicationService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using WebshopAPI.Core.DomainService;
using WebshopAPI.Core.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace WebshopAPI.Core.ApplicationService.Services.Tests
{
    [TestClass()]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> mockedProductRepo;
        private ProductService productService;

        [TestInitialize]
        public void SetUp()
        {
            // Given ProductRepository and ProductService
            mockedProductRepo = new Mock<IProductRepository>();
            productService = new ProductService(mockedProductRepo.Object);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Given a product with Title, brand, price, type and picture set
            Product product = new Product()
            {
                Title = "Title",
                Brand = "Brand",
                Price = "Price",
                Type = "Type",
                Picture = "Picture"
            };

            // Given Task with result of a single product
            var taskProduct = Task.FromResult(product);

            mockedProductRepo.Setup(repo => repo.Create(
               // When called with given product
               It.Is<Product>(prod => prod == product)
               )).Returns(taskProduct);

            // When we call Create with given product 
            var response = productService.Create(product);

            // Then we expect Response to be Task with single product
            var expected = taskProduct.Result;
            Assert.AreEqual(expected, response);
        }

        [TestMethod()]
        [DataRow("", "brand", "price", "type", "picture", DisplayName = "Missing title")]
        [DataRow("title", "", "price", "type", "picture", DisplayName = "Missing brand")]
        [DataRow("title", "brand", "", "type", "picture", DisplayName = "Missing price")]
        [DataRow("title", "brand", "price", "", "picture", DisplayName = "Missing type")]
        [DataRow("title", "brand", "price", "type", "", DisplayName = "Missing picture")]
        public void CreateMissingValuesTest(
            string title, string brand, string price, string type, string picture)
        {
            // Given a product without Title, brand, price, type or picture 
            Product product = new Product
            {
                Title = title,
                Brand = brand,
                Price = price,
                Type = type,
                Picture = picture
            };

            // When we call Create with given product
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => productService.Create(product));

            // Then we expect exception to contain this message
            Assert.AreEqual("Product has to have brand, price, title, type and picture set", ex.Message);
        }

        [TestMethod()]
        public void GetProductsShouldReturnWantedAmountTest()
        {
            // Given expected amount of products
            int wantedAmount = 3;

            // Given Filter with amount set
            Filter filter = new Filter
            {
                Amount = wantedAmount
            };

            // Given Task with result of products 
            var taskProducts = Task.FromResult(CreateAmountOfProducts(wantedAmount));

            mockedProductRepo.Setup(repo => repo.GetAmountOfProduct(
                // When called with wantedAmount
                It.Is<int>(amount => amount == wantedAmount)
                )).Returns(taskProducts);

            // When we call GetProducts 
            var response = productService.GetProducts(filter);

            // Then we expect Response to be Task with products
            var expected = taskProducts.Result.ToList();
            Assert.IsTrue(expected.SequenceEqual(response));
        }

        [TestMethod()]
        public void GetProductsShouldCallSearchProductsAsync()
        {
            // Given Filter with SearchWord set
            var searchWord = "Title";
            Filter filter = new Filter
            {
                SearchWord = searchWord
            };

            // Given Task with result of products 
            Task<IEnumerable<Product>> taskProducts = Task.FromResult(CreateAmountOfProducts(1));

            mockedProductRepo.Setup(repo => repo.SearchProductsAsync(
                // When called with searchWord
                It.Is<string>(search => search == searchWord)
                )).Returns(taskProducts);

            // When we call GetProducts 
            var response = productService.GetProducts(filter);

            // Then we expect Response to be Task with products
            var expected = taskProducts.Result.ToList();
            Assert.IsTrue(expected.SequenceEqual(response));
        }

        [TestMethod()]
        public void GetProductsShouldGetPaginatedProducts()
        {
            // Given Filter with LastItemId set
            var LastItemId = "SomeID";
            Filter filter = new Filter
            {
                LastItemId = LastItemId
            };

            // Given Task with result of products
            Task<IEnumerable<Product>> taskProducts = Task.FromResult(CreateAmountOfProducts(1));

            mockedProductRepo.Setup(repo => repo.GetProductsAsync(
                // When called with filter
                It.Is<Filter>(filt => filt == filter)
                )).Returns(taskProducts);

            // When we call GetProducts 
            var response = productService.GetProducts(filter);

            // Then we expect Response to be Task with products
            var expected = taskProducts.Result.ToList();
            Assert.IsTrue(expected.SequenceEqual(response));
        }

        [TestMethod()]
        public void GetProductsShouldGetFirstPaginatedProduct()
        {
            // Given Filter with only orderBy set
            var order = "Price";
            Filter filter = new Filter
            {
                OrderBy = order
            };

            // Given Task with result of products
            Task<IEnumerable<Product>> taskProducts = Task.FromResult(CreateAmountOfProducts(1));

            mockedProductRepo.Setup(repo => repo.GetProductsFirstPageAsync(
                // When called with orderBy value
                It.Is<string>(orderBy => orderBy == order)
                )).Returns(taskProducts);

            // When we call GetProducts 
            var response = productService.GetProducts(filter);

            // Then we expect Response to be Task with products
            var expected = taskProducts.Result.ToList();
            Assert.IsTrue(expected.SequenceEqual(response));
        }

        [TestMethod()]
        public void GetProductsShouldThrowExceptionIfNoFilterIsGiven()
        {
            // When we call GetProducts with filter without values
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => productService.GetProducts(new Filter()));

            // Then we expect exception to contain this message
            Assert.AreEqual("Filter must have some value", ex.Message);
        }

        [TestMethod()]
        public void GetByIdShouldReturnProductTest()
        {
            // Given id
            var id = "SomeID";

            // Given Task with result of specific product 
            Task<Product> taskProduct = Task.FromResult(new Product());

            mockedProductRepo.Setup(repo => repo.GetById(
                // When called with product's id
                It.Is<string>(productId => productId == id)
                )).Returns(taskProduct);

            // When we call GetById 
            var response = productService.GetById(id);

            // Then we expect to get a desired product
            Assert.AreEqual(taskProduct.Result, response);
        }


        [TestMethod()]
        public void GetByIdShouldThrowExceptionIfIdIsNotSetTest()
        {
            // Given empty string as product's id
            var id = "";

            // Given Task with result of specific product
            Task<Product> taskProduct = Task.FromResult(new Product());

            mockedProductRepo.Setup(repo => repo.GetById(
                // When called with product's id as empty string
                It.Is<string>(productId => productId == id)
                )).Returns(taskProduct);

            // When we call GetById with empty string
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => productService.GetById(id));

            // Then we expect exception to contain this message
            Assert.AreEqual("Product's id cannot be empty", ex.Message);
        }

        [TestMethod()]
        public void UpdateShouldThrowExceptionIfProductIdDoesNotMatchTest()
        {
            // Given random prouct's id
            var id = "RandomId";

            // Given Product With Correct ID
            Product product = new Product
            {
                Id = "CorrectID"
            };

            // Given Task with product
            Task<Product> taskUpdateProduct = Task.FromResult(product);

            mockedProductRepo.Setup(repo => repo.Update(
                // When called with product's id and product
                It.Is<string>(productId => productId == id),
                It.Is<Product>(productGiven => productGiven == product)
                )).Returns(taskUpdateProduct);

            // When we call Update with id and product
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => productService.Update(id, product));

            // Then we expect exception to contain this message
            Assert.AreEqual("Provided id and Product's id must match and can't be empty", ex.Message);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Given correct prouct's id
            var id = "CorrectId";

            // Given Product With correct ID
            Product product = new Product
            {
                Id = "CorrectId",
                Title = "Title",
                Brand = "brand",
                Price = "PRice",
                Type = "type",
                PictureUrl = "pictureUrl"
            };

            // Given Task with product 
            Task<Product> taskUpdateProduct = Task.FromResult(product);

            mockedProductRepo.Setup(repo => repo.Update(
                // When called with product's id and product
                It.Is<string>(productId => productId == id),
                It.Is<Product>(productGiven => productGiven == product)
                )).Returns(taskUpdateProduct);

            // When we call method Update with id and product
            var response = productService.Update(id, product);

            // Then we expect response to be updated product  
            Assert.AreEqual(taskUpdateProduct.Result, response);
        }


        [TestMethod()]
        [DataRow("", "brand", "price", "type", "pictureUrl", DisplayName = "Missing title")]
        [DataRow("title", "", "price", "type", "pictureUrl", DisplayName = "Missing brand")]
        [DataRow("title", "brand", "", "type", "pictureUrl", DisplayName = "Missing price")]
        [DataRow("title", "brand", "price", "", "pictureUrl", DisplayName = "Missing type")]
        [DataRow("title", "brand", "price", "type", "", DisplayName = "Missing pictureUrl")]
        public void UpdateShouldThrowExceptionIfMissingCoreValuesTest(
            string title, string brand, string price, string type, string pictureUrl)
        {
            // Given random prouct's id
            var id = "CorrectId";

            // Given Product With Correct ID
            Product product = new Product
            {
                Id = id,
                Title = title,
                Brand = brand,
                Price = price,
                Type = type,
                PictureUrl = pictureUrl
            };

            // When we call Update with id and product
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => productService.Update(id, product));

            // Then we expect exception to contain this message
            Assert.AreEqual("Product has to have brand, price, title, type and pictureUrl set", ex.Message);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Given correct prouct's id
            var id = "CorrectId";

            // Given Task with true value 
            Task<bool> taskDeleteProduct = Task.FromResult(true);

            mockedProductRepo.Setup(repo => repo.Delete(
                // When called with product's id
                It.Is<string>(productId => productId == id)
                )).Returns(taskDeleteProduct);

            // When we call method Delete with id
            var response = productService.Delete(id);

            // Then we expect response to true
            Assert.IsTrue(response);
        }

        [TestMethod()]
        public void DeleteShouldThrowExceptionIfIdIsMissingTest()
        {
            // When we call Delete with id that is empty
            // Then we expect MissingFieldException to be thrown
            var ex = Assert.ThrowsException<MissingFieldException>(() => productService.Delete(""));

            // Then we expect exception to contain this message
            Assert.AreEqual("Product's id has to have value", ex.Message);
        }

        /// <summary>
        /// Creates wanted amount of products, puts them in the list and returns it
        /// </summary>
        /// <param name="amount">Wanted amount of products</param>
        /// <returns>A list of products</returns>
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