using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        /** BasketController's constructor with DI*/
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        // GET api/basket/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Product>> Get(string id)
        {
            try
            {
                // Get Basket with provided id
                return _basketService.GetBasketById(id);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }


        // PUT api/basket/5?productId=productId
        [HttpPut("{id}")]
        public ActionResult<bool> Put(string id, [FromQuery] string productId)
        {
            try
            {
                // Add Product's id to the basket
                return _basketService.AddProductToBasket(id, productId);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

        // DELETE api/basket/5?productId=productId
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id, [FromQuery] string productId)
        {
            try
            {
                // Remove product's id from basket
                return _basketService.RemoveProductFromBasket(id, productId);
            }
            catch (Exception e)
            {
                // Return message to the client explaining the issue
                return StatusCode(400, e.Message);
            }
        }

    }
}