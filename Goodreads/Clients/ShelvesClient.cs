using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;
using System.Net;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Shelf endpoint of the Goodreads API.
    /// </summary>
    public class ShelvesClient : IShelvesClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShelvesClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public ShelvesClient(IConnection connection)
        {
            Connection = connection;
        }
                
        /// <summary>
        /// Gets a paginated list of shelves for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>A paginated list of the user's shelves.</returns>
        public Task<PaginatedList<UserShelf>> GetListOfUserShelves(int userId, int page = 1)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "user_id", Value = userId, Type = ParameterType.QueryString },
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<PaginatedList<UserShelf>>("shelf/list", parameters, null, "shelves");
        }

        /// <summary>
        /// Add a book to a shelf. This method can also be used to remove from shelf.
        /// </summary>
        /// <param name="shelf">Name of the shelf.</param>
        /// <param name="bookId">Id of the book to add to the shelf.</param>
        /// <param name="action">This null unless you're removing from a shelf. If removing, set this to 'remove'.</param>
        /// <returns>True if the add or remove succeeded, false otherwise.</returns>
        public async Task<bool> AddBookToShelf(string shelf, int bookId, string action = null)
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
    }
}
