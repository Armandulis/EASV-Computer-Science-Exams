using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.ApplicationService;
using WebShop.Core.ApplicationService.Services;
using WebShop.Core.Entity;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService) {
            _reviewService = reviewService;
        }

        // GET: api/Review
        [HttpGet]
        public IEnumerable<Review> Get([FromQuery] int userId, int productId)
        {
            if (userId > 0)
            {
                return _reviewService.GetReviewsByUser(userId);
            }
            if(productId > 0)
            {
                return _reviewService.GetReviewsByProduct(productId);
            }
            return _reviewService.GetAllReviews();
        }

        // GET: api/Review/5
        [HttpGet("{id}", Name = "GetReview")]
        public ActionResult<Review> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0");
            else return _reviewService.GetReview(id);
        }       

        // POST: api/Review
        [Authorize]
        [HttpPost]
        public ActionResult<Review> Post([FromBody] Review review)
        {
            try
            {
                return _reviewService.CreateReview(review);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            
        }

        // PUT: api/Review/5
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Review> Put(int id, [FromBody] Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }
            return _reviewService.UpdateReview(review);
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<Review> Delete(int id)
        {
            var review = _reviewService.DeleteReview(id);
            if (review == null)
            {
                return StatusCode(404, "Did not find review with ID " + id);
            }

            return review;

        }
    }
}
