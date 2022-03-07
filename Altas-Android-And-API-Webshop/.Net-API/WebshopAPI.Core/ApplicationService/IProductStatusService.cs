using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.ApplicationService
{
    public interface IProductStatusService
    {
        /// <summary>
        /// Valides given userId and calls repo to recieve List of productStatuses that belongs to the user
        /// </summary>
        /// <param name="userId">User's id</param>
        /// <returns>A list of user's productStatuses</returns>
        List<ProductStatus> GetAllUserProductStatuses(string userId);

        /// <summary>
        /// Valides given productStatus values and calls repo to update them
        /// </summary>
        /// <param name="productStatus">productStatus with new values that will be updated</param>
        /// <returns>True if task was successful</returns>
        bool UpdateProductStatusStatus(ProductStatus productStatus);

        /// <summary>
        /// Validates provided id and calls repo to remove product status from DB
        /// </summary>
        /// <param name="productStatusId">id of product Status that will be removed</param>
        /// <returns>True if task was successful</returns>
        bool RemoveProductStatus(string productStatusId);

        /// <summary>
        /// Validate new prductStatusValues and cal lrepo to create new porudct status
        /// </summary>
        /// <param name="newProductStatus">ProductStatus that will be created</param>
        /// <returns>True if it was successful</returns>
        bool CreateProductStatus(ProductStatus newProductStatus);

        /// <summary>
        /// Calls repo to get paginated productStatuses
        /// </summary>
        /// <param name="filter">filter for pagination</param>
        /// <returns>Paginated productStatus List</returns>
        List<ProductStatus> GetAllPaginatedProductStatuses(Filter filter);
    }
}
