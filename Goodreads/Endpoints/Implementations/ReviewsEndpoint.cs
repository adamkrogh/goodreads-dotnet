using Goodreads.Extensions;
using Goodreads.Helpers;
using Goodreads.Http;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Review endpoint of the Goodreads API.
    /// </summary>
    internal sealed class ReviewsEndpoint : Endpoint, IOAuthReviewsEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewsEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public ReviewsEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get a review by a Goodreads review id with paginated comments.
        /// </summary>
        /// <param name="reviewId">The id of the review.</param>
        /// <param name="commentsPage">The page of comments to fetch.</param>
        /// <returns>A review with the matching id.</returns>
        public async Task<ReviewDetails> GetById(long reviewId, int commentsPage)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = reviewId, Type = ParameterType.QueryString },
                new Parameter { Name = "page", Value = commentsPage, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<ReviewDetails>("review/show", parameters, null, "review").ConfigureAwait(false);
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
        public async Task<ReviewDetails> GetByUserIdAndBookId(long userId, long bookId, bool findReviewOnDifferentEdition)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "user_id", Value = userId, Type = ParameterType.QueryString },
                new Parameter { Name = "book_id", Value = bookId, Type = ParameterType.QueryString },
                new Parameter { Name = "include_review_on_work", Value = findReviewOnDifferentEdition, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<ReviewDetails>("review/show_by_user_and_book", parameters, null, "review").ConfigureAwait(false);
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
        public async Task<PaginatedList<Review>> GetListByUser(
            long userId,
            string shelfName,
            SortReviewsList? sort,
            string searchQuery,
            Order? order,
            int? page,
            int? pageSize)
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

            return await Connection.ExecuteRequest<PaginatedList<Review>>("review/list", parameters, null, "reviews").ConfigureAwait(false);
        }

        /// <summary>
        /// Get the most recent reviews that have been posted to Goodreads, for all users.
        /// </summary>
        /// <returns>The latest reviews that have been posted to Goodreads.</returns>
        public async Task<IReadOnlyList<Review>> GetRecentReviewsForAllMembers()
        {
            var reviews = await Connection.ExecuteRequest<PaginatedList<Review>>("review/recent_reviews", null, null, "reviews").ConfigureAwait(false);
            if (reviews != null)
            {
                return reviews.List;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Create a review for the authenticated user on the given book with some optional information.
        /// </summary>
        /// <param name="bookId">The book to create the review on.</param>
        /// <param name="reviewText">The body text of the review.</param>
        /// <param name="rating">The star rating the user gave the review. From 0 (no rating) to 5 (highest rating).</param>
        /// <param name="dateRead">The date the user read the book on.</param>
        /// <param name="shelfName">The shelf name to add the review to.</param>
        /// <returns>If successful, returns the id of the created review, null otherwise.</returns>
        public async Task<long?> Create(
            long bookId,
            string reviewText,
            int? rating,
            DateTime? dateRead,
            string shelfName)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "book_id", Value = bookId, Type = ParameterType.GetOrPost }
            };

            if (!string.IsNullOrWhiteSpace(reviewText))
            {
                parameters.Add(new Parameter { Name = "review[review]", Value = reviewText, Type = ParameterType.GetOrPost });
            }

            if (rating.HasValue)
            {
                parameters.Add(new Parameter { Name = "review[rating]", Value = rating.Value, Type = ParameterType.GetOrPost });
            }

            if (dateRead.HasValue)
            {
                parameters.Add(new Parameter
                {
                    Name = "review[read_at]",
                    Value = dateRead.Value.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture),
                    Type = ParameterType.GetOrPost
                });
            }

            if (!string.IsNullOrWhiteSpace(shelfName))
            {
                parameters.Add(new Parameter { Name = "shelf", Value = shelfName, Type = ParameterType.GetOrPost });
            }

            var response = await Connection.ExecuteRaw("review.xml", parameters, Method.POST).ConfigureAwait(false);
            if (response != null && response.StatusCode == HttpStatusCode.Created)
            {
                try
                {
                    var document = XDocument.Parse(response.Content);
                    if (document != null)
                    {
                        var reviewElement = document.Element("review");
                        if (reviewElement != null)
                        {
                            return reviewElement.ElementAsNullableLong("id");
                        }
                    }
                }
                catch
                {
                }
            }

            return null;
        }

        /// <summary>
        /// Edit a review with the given id.
        /// </summary>
        /// <param name="reviewId">The Goodreads review id of the review to edit.</param>
        /// <param name="reviewText">The body text of the review. Pass an empty string to clear the review text completely.</param>
        /// <param name="rating">The star rating the user gave the review. Pass a rating of 0 to remove a rating completely.</param>
        /// <param name="dateRead">The date the user read the book on.</param>
        /// <param name="shelfName">The shelf name to add the review to.</param>
        /// <returns>True if the edit succeeded, false otherwise.</returns>
        public async Task<bool> Edit(
            long reviewId,
            string reviewText,
            int? rating,
            DateTime? dateRead,
            string shelfName)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = reviewId, Type = ParameterType.UrlSegment }
            };

            if (reviewText != null)
            {
                parameters.Add(new Parameter { Name = "review[review]", Value = reviewText, Type = ParameterType.GetOrPost });
            }

            if (rating.HasValue)
            {
                parameters.Add(new Parameter { Name = "review[rating]", Value = rating.Value, Type = ParameterType.GetOrPost });
            }

            if (dateRead.HasValue)
            {
                parameters.Add(new Parameter
                {
                    Name = "review[read_at]",
                    Value = dateRead.Value.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture),
                    Type = ParameterType.GetOrPost
                });
            }

            if (!string.IsNullOrWhiteSpace(shelfName))
            {
                parameters.Add(new Parameter { Name = "shelf", Value = shelfName, Type = ParameterType.GetOrPost });
            }

            var response = await Connection.ExecuteRaw("review/{id}.xml", parameters, Method.POST).ConfigureAwait(false);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
