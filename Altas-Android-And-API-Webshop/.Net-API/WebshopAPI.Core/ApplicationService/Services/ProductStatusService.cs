using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebshopAPI.Core.DomainService;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.ApplicationService.Services
{
    public class ProductStatusService : IProductStatusService
    {
        private readonly IProductStatusRepository _productStatusRepo;
        public ProductStatusService(IProductStatusRepository productStatusRepo)
        {
            _productStatusRepo = productStatusRepo;
        }

        /** Valides userId and calls repo to get ProductStatus*/
        public List<ProductStatus> GetAllUserProductStatuses(string userId)
        {
            // Validate Id
            if (userId.Equals(""))
            {
                throw new MissingFieldException("User's id must be set");
            }

            // Get ProductStatus
            return _productStatusRepo.GetAllProductStatus(userId).Result.ToList();
        }

        /** Validate ProductSatus vlaues and update them */
        public bool UpdateProductStatusStatus(ProductStatus productStatus)
        {
            // Validate productStatus values
            if (productStatus.Product == null ||
                productStatus.UserId.Equals("") ||
                productStatus.PurchaseDate.Equals(""))
            {

                throw new MissingFieldException("You must provide a product that was ordered, purchase date, and user's id");
            }

            // Update productStatus status
            return _productStatusRepo.UpdateProductStatus(productStatus).Result;
        }

        /** Validate provided values and remove productStatus */
        public bool RemoveProductStatus(string productStatusId)
        {
            // Validate provided productStatus id
            if (productStatusId.Equals(""))
            {

                throw new MissingFieldException("Product's id must be provided");
            }

            // Remove ProductStatus
            return _productStatusRepo.RemoveProductStatus(productStatusId).Result;
        }

        /** Valides provided values and calls repo to create productStatus  */
        public bool CreateProductStatus(ProductStatus newProductStatus)
        {
            // Validate Values
            if (newProductStatus.Product.Id.Equals("") ||
                 newProductStatus.UserId.Equals(""))
            {

                throw new MissingFieldException("User's and products id is required to create ProductStatus");
            }

            // Set purchase date
            newProductStatus.PurchaseDate = GetCurrentDate();

            // Create ProductStatus
            return _productStatusRepo.CreateProductStatus(newProductStatus).Result;
        }


        /** Calls repo to get paginated productStatuses**/
        public List<ProductStatus> GetAllPaginatedProductStatuses(Filter filter)
        {
            // Get ProductStatus
            return _productStatusRepo.GetAllPaginatedProductStatuses(filter).Result.ToList();
        }

        /// <summary>
        /// Creates Current date as string
        /// </summary>
        /// <returns>Returns string of current date</returns>
        private string GetCurrentDate()
        {
            // Get current DateTime
            DateTime aDate = DateTime.Now;

            // Format Datetime in d/m/y formats and display them  
            return aDate.ToString("dd/MM/yyyy");
        }
    }
}
