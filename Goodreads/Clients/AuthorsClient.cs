using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Author endpoint of the Goodreads API.
    /// </summary>
    public class AuthorsClient : IAuthorsClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorsClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public AuthorsClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Get the author information for the given Id.
        /// </summary>
        /// <param name="authorId">The Goodreads Id for the desired author.</param>
        /// <returns>An async task returning the desired author information.</returns>
        public Task<Author> GetByAuthorId(int authorId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = authorId, Type = ParameterType.QueryString }
            };
            return Connection.ExecuteRequest<Author>("author/show.xml", parameters, null, "author");
        }
    }
}
