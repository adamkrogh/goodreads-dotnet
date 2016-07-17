using System;
using Goodreads.Clients;
using Goodreads.Http;
using RestSharp;
using RestSharp.Authenticators;

namespace Goodreads
{
    /// <summary>
    /// The client API class for accessing the Goodreads API.
    /// </summary>
    public class GoodreadsClient : IGoodreadsClient
    {
        private readonly string GoodreadsUrl = "https://www.goodreads.com/";

        /// <summary>
        /// Initializes a new instance of the <see cref="GoodreadsClient"/> class.
        /// This constructor doesn't used OAuth permissions and can be used for public methods.
        /// </summary>
        /// <param name="apiKey">Your Goodreads API key.</param>
        /// <param name="apiSecret">Your Goodreads API secret.</param>
        public GoodreadsClient(string apiKey, string apiSecret) : this(apiKey, apiSecret, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoodreadsClient"/> class.
        /// Use this constructor if you already have OAuth permissions for the user.
        /// </summary>
        /// <param name="apiKey">Your Goodreads API key.</param>
        /// <param name="apiSecret">Your Goodreads API secret.</param>
        /// <param name="accessToken">The user's OAuth access token.</param>
        /// <param name="accessSecret">The user's OAuth access secret.</param>
        public GoodreadsClient(string apiKey, string apiSecret, string accessToken, string accessSecret)
        {
            var client = new RestClient(new Uri(GoodreadsUrl))
            {
                UserAgent = "goodreads-dotnet"
            };

            client.AddDefaultParameter("key", apiKey, ParameterType.QueryString);
            client.AddDefaultParameter("format", "xml", ParameterType.QueryString);

            var apiCredentials = new ApiCredentials(client, apiKey, apiSecret, accessToken, accessSecret);

            // Setup the OAuth authenticator if they have passed on the appropriate tokens
            if (!string.IsNullOrWhiteSpace(accessToken) &&
                !string.IsNullOrWhiteSpace(accessSecret))
            {
                client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                    apiKey, apiSecret, accessToken, accessSecret);
            }

            Connection = new Connection(client, apiCredentials);
            Authors = new AuthorsClient(Connection);
            Books = new BooksClient(Connection);
            Shelves = new ShelvesClient(Connection);
            Users = new UsersClient(Connection);
            Reviews = new ReviewsClient(Connection);
            Series = new SeriesClient(Connection);
        }

        /// <summary>
        /// The connection to the Goodreads API.
        /// </summary>
        public IConnection Connection { get; private set; }

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

        /// <summary>
        /// API Client for the Goodreads Users endpoint.
        /// </summary>
        public IUsersClient Users { get; private set; }

        /// <summary>
        /// API Client for the Goodreads Reviews endpoint.
        /// </summary>
        public IReviewsClient Reviews { get; private set; }

        /// <summary>
        /// API Client for the Goodreads Series endpoint.
        /// </summary>
        public ISeriesClient Series { get; private set; }
    }
}
