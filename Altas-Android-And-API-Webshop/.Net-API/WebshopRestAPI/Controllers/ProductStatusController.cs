using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebshopAPI.Core.ApplicationService;
using WebshopAPI.Core.Entity;

namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : ControllerBase
    {
        private readonly IProductStatusService _productStatusService;

        // Controller's constructor with DI
        public ProductStatusController(IProductStatusService productStatusService)
        {
            _productStatusService = productStatusService;
        }

        // POST api/productStatus/id
        [HttpPost]
        public ActionResult<bool> Post([FromBody] ProductStatus newProductStatus)
        {
            try
            {
                return _productStatusService.CreateProductStatus(newProductStatus);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // GET api/productStatus/5
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<ProductStatus>> Get(string userId)
        {
            try
            {
                // Get ProductStatus with provided user's id
                return _productStatusService.GetAllUserProductStatuses(userId);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // GET api/productStatus
        [HttpGet]
        public ActionResult<IEnumerable<ProductStatus>> Get([FromQuery] Filter filter)
        {
            try
            {
                // Get paginated ProductStatus - only used by admins and emplyees
                return _productStatusService.GetAllPaginatedProductStatuses(filter);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // PUT api/productStatus
        [HttpPut]
        public ActionResult<bool> Put([FromBody] ProductStatus productStatus)
        {
            try
            {
                // Update ProductStatus
                return _productStatusService.UpdateProductStatusStatus(productStatus);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // DELETE api/productStatus/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            try
            {
                // Remove ProductSTatus
                return _productStatusService.RemoveProductStatus(id);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }
    }
}