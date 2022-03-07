using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopAPI.Core.ApplicationService;
using WebshopAPI.Core.Entity;

namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Controller's constructor with DI
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // When you want to get products, use:
        // GET api/products?OrderBy=Price&SearchWord= for returning all products.
        // GET api/products?OrderBy=Price&LastItemId=LastItemId
        // GET api/products?OrderBy=Price (OrderBy is always required)
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get([FromQuery] Filter filter)
        {
            try
            {
                // Get paginated products
                return _productService.GetProducts(filter);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(string id)
        {
            try
            {
                // Get Product by ID

                return _productService.GetById(id);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // POST api/products
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product newProduct)
        {
            try
            {
                // Create product
                return _productService.Create(newProduct);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public ActionResult<Product> Put(string id, [FromBody] Product productNewValues)
        {
            try
            {
                // Update product
                return _productService.Update(id, productNewValues);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            try
            {
                // Delete product
                return _productService.Delete(id);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }
    }
}
