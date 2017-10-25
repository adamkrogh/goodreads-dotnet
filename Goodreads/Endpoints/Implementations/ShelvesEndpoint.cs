using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Shelf endpoint of the Goodreads API.
    /// </summary>
    internal sealed class ShelvesEndpoint : Endpoint, IOAuthShelvesEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShelvesEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public ShelvesEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Gets a paginated list of shelves for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>A paginated list of the user's shelves.</returns>
        public async Task<PaginatedList<UserShelf>> GetListOfUserShelves(long userId, int page)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "user_id", Value = userId, Type = ParameterType.QueryString },
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<PaginatedList<UserShelf>>("shelf/list", parameters, null, "shelves").ConfigureAwait(false);
        }

        /// <summary>
        /// Add a book to a shelf. This method can also be used to remove from shelf.
        /// </summary>
        /// <param name="shelf">Name of the shelf.</param>
        /// <param name="bookId">Id of the book to add to the shelf.</param>
        /// <param name="action">This null unless you're removing from a shelf. If removing, set this to 'remove'.</param>
        /// <returns>True if the add or remove succeeded, false otherwise.</returns>
        public async Task<bool> AddBookToShelf(string shelf, long bookId, string action)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "name", Value = shelf, Type = ParameterType.QueryString },
                new Parameter { Name = "book_id", Value = bookId, Type = ParameterType.QueryString },
                new Parameter { Name = "a", Value = action ?? string.Empty, Type = ParameterType.QueryString }
            };

            var response = await Connection.ExecuteRaw("shelf/add_to_shelf", parameters, Method.POST).ConfigureAwait(false);

            return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created;
        }

        /// <summary>
        /// Add a list of books to many current user's shelves.
        /// </summary>
        /// <param name="shelves">List of shelf names.</param>
        /// <param name="bookIds">List of book ids.</param>
        /// <returns>True if the add succeeded, false otherwise.</returns>
        public async Task<bool> AddBooksToShelves(string[] shelves, long[] bookIds)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "shelves", Value = string.Join(",", shelves), Type = ParameterType.QueryString },
                new Parameter { Name = "bookids", Value = string.Join(",", bookIds), Type = ParameterType.QueryString }
            };

            var response = await Connection.ExecuteRaw("shelf/add_books_to_shelves", parameters, Method.POST).ConfigureAwait(false);

            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Add a user book shelf.
        /// </summary>
        /// <param name="shelf">Name of the user shelf.</param>
        /// <param name="exclusive">Determine whether shelf is exclusive.</param>
        /// <param name="sortable">Determine whether shelf is sortable.</param>
        /// <param name="featured">Determine whether shelf is featured.</param>
        /// <returns>The created user shelf.</returns>
        public async Task<UserShelf> AddShelf(string shelf, bool exclusive, bool sortable, bool featured)
        {
            var parameters = new List<Parameter>
            {
                new Parameter
                {
                    Name = "user_shelf[name]",
                    Value = shelf,
                    Type = ParameterType.QueryString
                },
                new Parameter
                {
                    Name = "user_shelf[exclusive_flag]",
                    Value = exclusive ? "true" : "false",
                    Type = ParameterType.QueryString
                },
                new Parameter
                {
                    Name = "user_shelf[sortable_flag]",
                    Value = sortable ? "true" : "false",
                    Type = ParameterType.QueryString
                },
                new Parameter
                {
                    Name = "user_shelf[featured]",
                    Value = featured ? "true" : "false",
                    Type = ParameterType.QueryString
                }
            };

            return await Connection.ExecuteRequest<UserShelf>(
                "user_shelves",
                parameters,
                null,
                "user_shelf",
                Method.POST)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Edit a user book shelf.
        /// </summary>
        /// /// <param name="shelfId">Id of the user shelf.</param>
        /// <param name="shelf">Name of the shelf.</param>
        /// <param name="exclusive">Determine whether shelf is exclusive.</param>
        /// <param name="sortable">Determine whether shelf is sortable.</param>
        /// <param name="featured">Determine whether shelf is featured.</param>
        /// <returns>True if the edit succeeded, false otherwise.</returns>
        public async Task<bool> EditShelf(long shelfId, string shelf, bool exclusive, bool sortable, bool featured)
        {
            var endpoint = $"user_shelves/{shelfId}";

            var parameters = new List<Parameter>
            {
                new Parameter
                {
                    Name = "user_shelf[name]",
                    Value = shelf,
                    Type = ParameterType.QueryString
                },
                new Parameter
                {
                    Name = "user_shelf[exclusive_flag]",
                    Value = exclusive ? "true" : "false",
                    Type = ParameterType.QueryString
                },
                new Parameter
                {
                    Name = "user_shelf[sortable_flag]",
                    Value = sortable ? "true" : "false",
                    Type = ParameterType.QueryString
                },
                new Parameter
                {
                    Name = "user_shelf[featured]",
                    Value = featured ? "true" : "false",
                    Type = ParameterType.QueryString
                }
            };

            var response = await Connection.ExecuteRaw(endpoint, parameters, Method.PUT).ConfigureAwait(false);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
