using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Goodreads.Exceptions;
using Goodreads.Extensions;
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

        /// <summary>
        /// Get the most recent reviews that have been posted to Goodreads, for all users.
        /// </summary>
        /// <returns>The latest reviews that have been posted to Goodreads.</returns>
        public async Task<IReadOnlyList<Review>> GetRecentReviewsForAllMembers()
        {
            var reviews = await Connection.ExecuteRequest<PaginatedList<Review>>("review/recent_reviews", null, null, "reviews");
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
        public async Task<int?> Create(
            int bookId,
            string reviewText = null,
            int? rating = null,
            DateTime? dateRead = null,
            string shelfName = null)
        {
            if (!Connection.IsAuthenticated)
            {
                throw new ApiException("User authentication (using OAuth) is required to create a review.");
            }

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

            var response = await Connection.ExecuteRaw("review.xml", parameters, Method.POST);
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
                            return reviewElement.ElementAsNullableInt("id");
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
            int reviewId,
            string reviewText = null,
            int? rating = null,
            DateTime? dateRead = null,
            string shelfName = null)
        {
            if (!Connection.IsAuthenticated)
            {
                throw new ApiException("User authentication (using OAuth) is required to edit a review.");
            }

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

            var response = await Connection.ExecuteRaw("review/{id}.xml", parameters, Method.POST);

            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Delete the review with the given id.
        /// </summary>
        /// <param name="reviewId">The id of the review to delete.</param>
        /// <returns>True if the delete succeeded, false otherwise.</returns>
        /// <remarks>TODO: Goodreads returns strange errors for this endpoint and never works.
        /// I'll have to file a bug with them or post to their help forum...</remarks>
        public async Task<bool> Delete(int reviewId)
        {
            if (!Connection.IsAuthenticated)
            {
                throw new ApiException("User authentication (using OAuth) is required to delete a review.");
            }

            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = reviewId, Type = ParameterType.UrlSegment }
            };

            var response = await Connection.ExecuteRaw("review/destroy/{id}", parameters, Method.DELETE);

            return true;
        }
    }
}
