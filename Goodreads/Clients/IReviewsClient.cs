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
    }
}
