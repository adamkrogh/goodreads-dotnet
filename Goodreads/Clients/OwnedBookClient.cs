using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    public sealed class OwnedBookClient : IOwnedBookClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnedBookClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public OwnedBookClient(IConnection connection)
        {
            Connection = connection;
        }

        public async Task<PaginatedList<OwnedBook>> GetOwnedBooks(int userId, int page = 1)
        {
            var endpoint = @"owned_books/user";

            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = userId, Type = ParameterType.QueryString },
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<PaginatedList<OwnedBook>>(endpoint, parameters, null, "owned_books");
        }
    }
}
