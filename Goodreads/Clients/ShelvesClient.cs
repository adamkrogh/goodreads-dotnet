using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

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
    }
}
