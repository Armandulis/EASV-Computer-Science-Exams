using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebshopAPI.Core.ApplicationService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Core.DomainService;
using Moq;
using WebshopAPI.Core.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace WebshopAPI.Core.ApplicationService.Services.Tests
{
    [TestClass()]
    public class ProductStatusServiceTests
    {
        private Mock<IProductStatusRepository> mockedProductStatusRepo;
        private ProductStatusService productStatusService;

        [TestInitialize]
        public void SetUp()
        {
            // Given ProductStatusRepository and ProductStatusService
            mockedProductStatusRepo = new Mock<IProductStatusRepository>();
            productStatusService = new ProductStatusService(mockedProductStatusRepo.Object);
        }

        [TestMethod()]
        public void GetAllUserProductStatusesTest()
        {
            // Given user id
            string userId = "SomeRealId";

            // Given Task with list of productStatuses
            Task<IEnumerable<ProductStatus>> taskProductStatusCreate = Task.FromResult(CreateAmountOfProductsStatus(1));

            mockedProductStatusRepo.Setup(repo => repo.GetAllProductStatus(
                // When called with given userId
                It.Is<string>(usersId => usersId == userId)
                )).Returns(taskProductStatusCreate);

            // When called GetAllProductStatus
            var response = productStatusService.GetAllUserProductStatuses(userId);

            // Then we expect Response to be Task with productStatuses
            var expected = taskProductStatusCreate.Result.ToList();
            Assert.IsTrue(expected.SequenceEqual(response));
        }

        [TestMethod()]
        public void GetAllUserProductStatusesShouldThrowExceptionTest()
        {
            // Given empty id
            string userId = "";

            // When called GetAllProductStatuses
            var ex = Assert.ThrowsException<MissingFieldException>(() => productStatusService.GetAllUserProductStatuses(userId));

            // Then we expect exception to contain this message
            Assert.AreEqual("User's id must be set", ex.Message);
        }

        [TestMethod()]
        public void UpdateProductStatusStatusTest()
        {
            // Given a productStatus with product and user id
            ProductStatus productStatus = new ProductStatus()
            {
                Product = new Product(),
                UserId = "userId",
                PurchaseDate = "12-09-1989"
            };

            // Given Task with true because task was succesfull
            Task<bool> taskProductStatusUpdate = Task.FromResult(true);

            mockedProductStatusRepo.Setup(repo => repo.UpdateProductStatus(
                // When called with given ProductStatus
                It.Is<ProductStatus>(prodStatus => prodStatus == productStatus)
                )).Returns(taskProductStatusUpdate);

            // When called UpdateProductStatusStatus
            var response = productStatusService.UpdateProductStatusStatus(productStatus);

            // Then we expect Response to be true
            Assert.IsTrue(response);
        }

        [TestMethod()]
        [DataRow("", "purchaseDate", DisplayName = "Missing userId")]
        [DataRow("userId", "", DisplayName = "Missing purchaseDate")]
        public void UpdateProductStatusStatusShouldThrowExceptionTest(string userId, string purchaseDate)
        {
            // Given productStatus with missing values
            ProductStatus productStatus = new ProductStatus
            {
                Product = null,
                UserId = userId,
                PurchaseDate = purchaseDate
            };

            // When called UpdateProductStatusStatus
            var ex = Assert.ThrowsException<MissingFieldException>(() => productStatusService.UpdateProductStatusStatus(productStatus));

            // Then we expect exception to contain this message
            Assert.AreEqual("You must provide a product that was ordered, purchase date, and user's id", ex.Message);
        }

        [TestMethod()]
        public void RemoveProductStatusTest()
        {
            // Given a productStatusId
            string productStatusID = "SomeProductStatusId";

            // Given Task with true because task was succesfull
            Task<bool> taskProductStatusRemove = Task.FromResult(true);

            mockedProductStatusRepo.Setup(repo => repo.RemoveProductStatus(
                // When called with given ProductStatus
                It.Is<string>(id => id == productStatusID)
                )).Returns(taskProductStatusRemove);

            // When called RemoveProductStatus
            var response = productStatusService.RemoveProductStatus(productStatusID);

            // Then we expect Response to be true
            Assert.IsTrue(response);
        }

        [TestMethod()]
        public void RemoveProductStatusShouldThrowExceptionTest()
        {
            // Given empty productStatusId
            string productStatusId = "";

            // When called RemoveProductStatus
            var ex = Assert.ThrowsException<MissingFieldException>(() => productStatusService.RemoveProductStatus(productStatusId));

            // Then we expect exception to contain this message
            Assert.AreEqual("Product's id must be provided", ex.Message);
        }

        [TestMethod()]
        public void CreateProductStatusTest()
        {
            // Given a productID and productStatus with user's id set
            ProductStatus productStatus = new ProductStatus()
            {
                UserId = "realUserId",

                Product = new Product
                {
                    Id = "SomeProductId"
                }
            };

            // Given Task with true because task was succesfull
            Task<bool> taskProductStatusRemove = Task.FromResult(true);

            // Format Datetime in d/m/y formats and display them  
            DateTime aDate = DateTime.Now;
            var currentDate = aDate.ToString("dd/MM/yyyy");

            mockedProductStatusRepo.Setup(repo => repo.CreateProductStatus(
                // When called with given ProductStatus
                // Should also now have purchase date set
                It.Is<ProductStatus>(prodStatus => (productStatus.PurchaseDate.Equals(currentDate)))
                )).Returns(taskProductStatusRemove);

            // When called CreateProductStatus
            var response = productStatusService.CreateProductStatus(productStatus);

            // Then we expect Response to be true
            Assert.IsTrue(response);
        }

        [TestMethod()]
        [DataRow("", "productId", DisplayName = "Missing userId")]
        [DataRow("userId", "", DisplayName = "Missing ProductId")]
        public void CreateProductStatusShouldThrowExceptionTest(string userId, string productId)
        {
            // Given ProductStatus
            ProductStatus productStatus = new ProductStatus()
            {
                UserId = userId,
                Product = new Product
                {
                    Id = productId
                }

            };

            // When called CreateProductStatus with empty values
            var ex = Assert.ThrowsException<MissingFieldException>(() =>
                productStatusService.CreateProductStatus(productStatus));

            // Then we expect exception to contain this message
            Assert.AreEqual("User's and products id is required to create ProductStatus", ex.Message);
        }

        [TestMethod()]
        public void GetAllPaginatedProductStatusesTest()
        {
            // Given user id
            Filter filter = new Filter
            {
                LastItemId = "SomeRealId"
            };


            // Given Task with list of productStatuses
            Task<IEnumerable<ProductStatus>> taskProductStatusCreate = Task.FromResult(CreateAmountOfProductsStatus(1));

            mockedProductStatusRepo.Setup(repo => repo.GetAllPaginatedProductStatuses(
                // When called with given userId
                It.Is<Filter>(filter => filter.LastItemId == filter.LastItemId)
                )).Returns(taskProductStatusCreate);

            // When called GetAllPaginatedProductStatuses
            var response = productStatusService.GetAllPaginatedProductStatuses(filter);

            // Then we expect Response to be Task with productStatuses
            var expected = taskProductStatusCreate.Result.ToList();
            Assert.IsTrue(expected.SequenceEqual(response));
        }

        /// <summary>
        /// Creates wanted amount of products, puts them in the list and returns it
        /// </summary>
        /// <param name="amount">Wanted amount of products</param>
        /// <returns>A list of products</returns>
        private IEnumerable<ProductStatus> CreateAmountOfProductsStatus(int amount)
        {
            List<ProductStatus> productStatusList = new List<ProductStatus>();

            for (int i = 0; i < amount; i++)
            {
                ProductStatus productStatus = new ProductStatus()
                {
                    Id = "someId",
                    Product = new Product(),
                    Status = "Ready to pick up",
                    UserId = "RealUSerId"

                };
                productStatusList.Add(productStatus);
            }

            return productStatusList;
        }


    }
}