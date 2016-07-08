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
        /// Get a list of book reviews on a user's account. Several optional parameters
        /// allow for custom sorting and searching for this list.
        /// Users with private profiles only allow friends to list their books (via OAuth).
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
