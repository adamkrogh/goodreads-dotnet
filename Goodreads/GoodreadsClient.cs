using Goodreads.Clients;
using Goodreads.Http;
using RestSharp;

namespace Goodreads
{
    /// <summary>
    /// The client API class for accessing the Goodreads API.
    /// </summary>
    public class GoodreadsClient : IGoodreadsClient
    {
        private readonly string GoodreadsUrl = "https://www.goodreads.com/";
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoodreadsClient"/> class.
        /// </summary>
        /// <param name="key">Your Goodreads API key.</param>
        public GoodreadsClient(string key)
        {
            var client = new RestClient(GoodreadsUrl)
            {
                UserAgent = "goodreads-dotnet"
            };

            client.AddDefaultParameter("key", key, ParameterType.QueryString);
            client.AddDefaultParameter("format", "xml", ParameterType.QueryString);

            Connection = new Connection(client);

            Authors = new AuthorsClient(Connection);
            Books = new BooksClient(Connection);
            Shelves = new ShelvesClient(Connection);
        }

        /// <summary>
        /// API Client for the Goodreads Authors endpoint.
        /// </summary>
        public IAuthorsClient Authors { get; private set; }

        /// <summary>
        /// API Client for the Goodreads Books endpoint.
        /// </summary>
        public IBooksClient Books { get; private set; }

        /// <summary>
        /// API Client for the Goodreads Shelves endpoint.
        /// </summary>
        public IShelvesClient Shelves { get; private set; }
    }
}
