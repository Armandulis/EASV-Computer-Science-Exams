using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.DomainService
{
    public interface IReviewRepository
    {
        /// <summary>
        /// Add a new review to the database.
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        Review CreateReview(Review review);

        /// <summary>
        /// Get a review from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Review GetReview(int id);
        IEnumerable<Review> GetAllReviews();

        /// <summary>
        /// Update a review in the database.
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        Review UpdateReview(Review review);

        /// <summary>
        /// Delete a review from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Review DeleteReview(int id);

        /// <summary>
        /// Get all reviews of a product in the database.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IEnumerable<Review> GetReviewsByProduct(int productId);

        /// <summary>
        /// Get all reviews by a user in the database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Review> GetReviewsByUser(int userId);
    }
}
