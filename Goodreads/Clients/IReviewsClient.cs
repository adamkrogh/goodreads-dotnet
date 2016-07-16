using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Models.Request;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client interface for the Review endpoint of the Goodreads API.
    /// </summary>
    public interface IReviewsClient
    {
        /// <summary>
        /// Get a review by a Goodreads review id with paginated comments.
        /// </summary>
        /// <param name="reviewId">The id of the review.</param>
        /// <param name="commentsPage">The page of comments to fetch.</param>
        /// <returns>A review with the matching id.</returns>
        Task<ReviewDetails> GetById(int reviewId, int commentsPage = 1);

        /// <summary>
        /// Get a review for a user and book, and optionally find the review
        /// if it occurs on another edition of the book.
        /// </summary>
        /// <param name="userId">The user id that made the review.</param>
        /// <param name="bookId">The book id that the review is for.</param>
        /// <param name="findReviewOnDifferentEdition">If the review was not found on the
        /// given book id, search all editions of the book for the review.</param>
        /// <returns>A review that matches the given parameters.</returns>
        Task<ReviewDetails> GetByUserIdAndBookId(int userId, int bookId, bool findReviewOnDifferentEdition = false);

        /// <summary>
        /// Get a list of book reviews on a user's account. Several optional parameters
        /// allow for custom sorting and searching for this list.
        /// Users with private profiles only allow friends to list their books (via OAuth).
        /// Note: this endpoint doesn't include review comments.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="shelfName">The name of the shelf to list reviews from.</param>
        /// <param name="sort">The property to sort the reviews on.</param>
        /// <param name="searchQuery">A search query to match against the reviews.</param>
        /// <param name="order">The order to sort the reviews in.</param>
        /// <param name="page">The page of the reviews list to return.</param>
        /// <param name="pageSize">The number of reviews to return per page (from 1 to 200).</param>
        /// <returns>A paginated list of reviews for the user.</returns>
        Task<PaginatedList<Review>> GetListByUser(
            int userId,
            string shelfName = null,
            SortReviewsList? sort = null,
            string searchQuery = null,
            Order? order = null,
            int? page = null,
            int? pageSize = null);

        /// <summary>
        /// Get the most recent reviews that have been posted to Goodreads, for all users.
        /// </summary>
        /// <returns>The latest reviews that have been posted to Goodreads.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request.")]
        Task<IReadOnlyList<Review>> GetRecentReviewsForAllMembers();

        /// <summary>
        /// Create a review for the authenticated user on the given book with some optional information.
        /// </summary>
        /// <param name="bookId">The book to create the review on.</param>
        /// <param name="reviewText">The body text of the review.</param>
        /// <param name="rating">The star rating the user gave the review. From 0 (no rating) to 5 (highest rating).</param>
        /// <param name="dateRead">The date the user read the book on.</param>
        /// <param name="shelfName">The shelf name to add the review to.</param>
        /// <returns>If successful, returns the id of the created review, null otherwise.</returns>
        Task<int?> Create(
            int bookId,
            string reviewText = null,
            int? rating = null,
            DateTime? dateRead = null,
            string shelfName = null);

        /// <summary>
        /// Edit a review with the given id.
        /// </summary>
        /// <param name="reviewId">The Goodreads review id of the review to edit.</param>
        /// <param name="reviewText">The body text of the review.</param>
        /// <param name="rating">The star rating the user gave the review. From 0 (no rating) to 5 (highest rating).</param>
        /// <param name="dateRead">The date the user read the book on.</param>
        /// <param name="shelfName">The shelf name to add the review to.</param>
        /// <returns>True if the edit succeeded, false otherwise.</returns>
        Task<bool> Edit(
            int reviewId,
            string reviewText = null,
            int? rating = null,
            DateTime? dateRead = null,
            string shelfName = null);
    }
}
