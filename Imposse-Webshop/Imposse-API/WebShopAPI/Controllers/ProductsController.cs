using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.ApplicationService;
using WebShop.Core.Entity;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService service)
        {
            _productService = service;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetProducts([FromQuery] Filter filter)
        {
            if (filter.CurrentPage <= 0 || filter.ItemsPerPage <= 0)
            {
                return _productService.GetAllProducts();
            }
            return _productService.GetAllProducts(filter);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct([FromRoute] int id)
        {
            try
            {
                return _productService.GetProductById(id);
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }
        }

        // PUT: api/Products/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Product> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }   

            return _productService.UpdateProduct(product);
        }

        // POST: api/Products
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Product> PostProduct([FromBody] Product product)
        {
            try
            {
                return _productService.CreateProduct(product);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }

        }

        // DELETE: api/Products/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct([FromRoute] int id)
        {
            try
            {
                return _productService.DeleteProduct(id);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}