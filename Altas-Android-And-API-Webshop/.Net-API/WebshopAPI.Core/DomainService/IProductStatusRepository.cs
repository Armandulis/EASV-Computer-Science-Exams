using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.DomainService
{
    public interface IProductStatusRepository
    {
        /// <summary>
        /// Gets ProductStatus all product status that has user's id from the database and return list of ProductStatus
        /// </summary>
        /// <param name="userId">User's id who puched these iteams</param>
        /// <returns>A list of users productStatus</returns>
        Task<IEnumerable<ProductStatus>> GetAllProductStatus(string userId);

        /// <summary>
        /// Gets reference to productStatus document and removes it
        /// </summary>
        /// <param name="productStatusId">Id of productStatus document</param>
        /// <returns>true if deletion was succesful</returns>
        Task<bool> RemoveProductStatus(string productStatusId);

        /// <summary>
        /// Get productStatus document reference and update it
        /// </summary>
        /// <param name="productStatus">Product with new status value</param>
        /// <returns>True if task was succesfull</returns>
        Task<bool> UpdateProductStatus(ProductStatus productStatus);


        /// <summary>
        /// Creates a document for productStatus in database
        /// </summary>
        /// <param name="productStatus">Product status and user's id</param>
        /// <returns>True if creation was succesfull</returns>
        Task<bool> CreateProductStatus(ProductStatus productStatus);

        /// <summary>
        /// Orders documents by date and returns 10 productStatuses
        /// </summary>
        /// <param name="filter">filter with productStatus id of last paginated product, if need first page leave filter empty</param>
        /// <returns>A list of paginated productStatuses</returns>
        Task<IEnumerable<ProductStatus>> GetAllPaginatedProductStatuses(Filter filter);
    }
}
