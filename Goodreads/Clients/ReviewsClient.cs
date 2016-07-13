using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Helpers;
using Goodreads.Http;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Review endpoint of the Goodreads API.
    /// </summary>
    public class ReviewsClient : IReviewsClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewsClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public ReviewsClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Get a review by a Goodreads review id with paginated comments.
        /// </summary>
        /// <param name="reviewId">The id of the review.</param>
        /// <param name="commentsPage">The page of comments to fetch.</param>
        /// <returns>A review with the matching id.</returns>
        public Task<ReviewDetails> GetById(int reviewId, int commentsPage = 1)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = reviewId, Type = ParameterType.QueryString },
                new Parameter { Name = "page", Value = commentsPage, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<ReviewDetails>("review/show", parameters, null, "review");
        }

        /// <summary>
        /// Get a review for a user and book, and optionally find the review
        /// if it occurs on another edition of the book.
        /// </summary>
        /// <param name="userId">The user id that made the review.</param>
        /// <param name="bookId">The book id that the review is for.</param>
        /// <param name="findReviewOnDifferentEdition">If the review was not found on the
        /// given book id, search all editions of the book for the review.</param>
        /// <returns>A review that matches the given parameters.</returns>
        public Task<ReviewDetails> GetByUserIdAndBookId(int userId, int bookId, bool findReviewOnDifferentEdition = false)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "user_id", Value = userId, Type = ParameterType.QueryString },
                new Parameter { Name = "book_id", Value = bookId, Type = ParameterType.QueryString },
                new Parameter { Name = "include_review_on_work", Value = findReviewOnDifferentEdition, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<ReviewDetails>("review/show_by_user_and_book", parameters, null, "review");
        }

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
        public Task<PaginatedList<Review>> GetListByUser(
            int userId,
            string shelfName = null,
            SortReviewsList? sort = null,
            string searchQuery = null,
            Order? order = null,
            int? page = null,
            int? pageSize = null)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "v", Value = 2, Type = ParameterType.QueryString },
                new Parameter { Name = "id", Value = userId, Type = ParameterType.QueryString }
            };

            Action<string, object> addQueryString = (name, value) =>
            {
                parameters.Add(new Parameter { Name = name, Value = value, Type = ParameterType.QueryString });
            };

            if (!string.IsNullOrWhiteSpace(shelfName))
            {
                addQueryString("shelf", shelfName);
            }

            if (sort.HasValue)
            {
                addQueryString(EnumHelpers.QueryParameterKey<SortReviewsList>(), EnumHelpers.QueryParameterValue(sort.Value));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                addQueryString("search[query]", searchQuery);
            }

            if (order.HasValue)
            {
                addQueryString(EnumHelpers.QueryParameterKey<Order>(), EnumHelpers.QueryParameterValue(sort.Value));
            }

            if (page.HasValue)
            {
                addQueryString("page", page.Value);
            }

            if (pageSize.HasValue)
            {
                addQueryString("per_page", pageSize.Value);
            }

            return Connection.ExecuteRequest<PaginatedList<Review>>("review/list", parameters, null, "reviews");
        }
    }
}
