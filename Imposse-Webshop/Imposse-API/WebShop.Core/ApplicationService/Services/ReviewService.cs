using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebShop.Core.ApplicationService.Services;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService.Services
{
    public class ReviewService : IReviewService

    {
        readonly IReviewRepository _repo;

        public ReviewService(IReviewRepository repo) {
            _repo = repo;
        }

        public Review CreateReview(Review review)
        {
            if (review.User == null || review.User.Id <= 0)
                throw new InvalidDataException("To create a Review you need a User");
            if (review.Product == null)
                throw new InvalidDataException("Review needs a Product");
            if (review.Rating < 1 || review.Rating > 5)
                throw new InvalidDataException("Review must have a rating between 1 and 5");

                return _repo.CreateReview(review);
        }

        public Review DeleteReview(int id)
        {
            return _repo.DeleteReview(id);
        }

        public List<Review> GetAllReviews()
        {
           return _repo.GetAllReviews().ToList();
        }

        public Review GetReview(int id)
        {
            return _repo.GetReview(id);
        }

        public List<Review> GetReviewsByProduct(int productId)
        {
            return _repo.GetReviewsByProduct(productId).ToList();
        }

        public List<Review> GetReviewsByUser(int userId)
        {
            return _repo.GetReviewsByUser(userId).ToList();
        }

        public Review UpdateReview(Review review)
        {
            return _repo.UpdateReview(review);
        }
    }
}
