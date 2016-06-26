using System.Collections.Generic;
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

        /// <summary>
        /// Gets a paginated list of books written by the given author.
        /// </summary>
        /// <param name="authorId">The Goodreads author id.</param>
        /// <param name="page">The desired page from the paginated list of books.</param>
        /// <returns>A paginated list of books written by the author.</returns>
        public Task<PaginatedList<Book>> GetListByAuthorId(int authorId, int page)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "authorId", Value = authorId, Type = ParameterType.UrlSegment },
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<PaginatedList<Book>>("author/list/{authorId}", parameters, null, "author/books");
        }
    }
}
