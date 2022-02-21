using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;

namespace WebShop.Infrastructure.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {

        readonly WebShopContext _ctx;

        public ReviewRepository(WebShopContext ctx) {
            _ctx = ctx;
        }

        public Review CreateReview(Review review)
        {
            if (review.Product != null && review.User != null)
            {
                _ctx.Products.Attach(review.Product).State = EntityState.Unchanged;
                _ctx.Users.Attach(review.User).State = EntityState.Unchanged;
            }
            
            _ctx.Reviews.Attach(review).State = EntityState.Added;
            _ctx.SaveChanges();
            
            return review;
        }

        public Review DeleteReview(int id)
        {
            var deleteReview = _ctx.Reviews.Remove(GetReview(id)).Entity;
            _ctx.SaveChanges();
            return deleteReview;
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _ctx.Reviews;
        }

        public Review GetReview(int id)
        {
            return _ctx.Reviews.Include(r => r.User).Include(r => r.Product).FirstOrDefault(review => review.Id == id);
        }

        public IEnumerable<Review> GetReviewsByProduct(int productId)
        {
            return _ctx.Reviews.Where(r => r.Product.Id == productId).Include(r => r.User);
        }

        public IEnumerable<Review> GetReviewsByUser(int userId)
        {
            return _ctx.Reviews.Where(r => r.User.Id == userId).Include(r => r.Product);
        }

        public Review UpdateReview(Review review)
        {
            var updatedReview = _ctx.Reviews.Update(review).Entity;
            _ctx.SaveChanges();
            return updatedReview;
        }
    }
}
