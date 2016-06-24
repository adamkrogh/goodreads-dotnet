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
    /// The client class for the Book endpoint of the Goodreads API.
    /// </summary>
    public class BooksClient : IBooksClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public BooksClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Get book information by ISBN.
        /// </summary>
        /// <param name="isbn">The ISBN of the desired book.</param>
        /// <returns>An async task returning the desired book information.</returns>
        public Task<Book> GetByIsbn(string isbn)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "isbn", Value = isbn, Type = ParameterType.UrlSegment }
            };

            return Connection.ExecuteRequest<Book>("book/isbn/{isbn}.xml", parameters, null, "book");
        }
    }
}
