using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService
{
    public interface IReviewService
    {
        /// <summary>
        /// Add a new review to the database.
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        Review CreateReview(Review review);

        /// <summary>
        /// Get a review by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Review GetReview(int id);

        /// <summary>
        /// Get all reviews from the database.
        /// </summary>
        /// <returns></returns>
        List<Review> GetAllReviews();

        /// <summary>
        /// Update a review with new information.
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
        /// Get all reviews of a given product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<Review> GetReviewsByProduct(int productId);

        /// <summary>
        /// Get all reviews by a given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Review> GetReviewsByUser(int userId);
    }
}
