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
        /// The connection to the Goodreads API.
        /// </summary>
        private readonly IConnection connection;

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

            connection = new Connection(client, apiCredentials);
            Authors = new AuthorsClient(connection);
            Books = new BooksClient(connection);
            Shelves = new ShelvesClient(connection);
            Users = new UsersClient(connection);
            Reviews = new ReviewsClient(connection);
            Series = new SeriesClient(connection);
            AuthorsFollowing = new AuthorsFollowingClient(connection);
            Events = new EventsClient(connection);
            Followers = new FollowersClient(connection);
            Friends = new FriendsClient(connection);
            Notifications = new NotificationsClient(connection);
            Groups = new GroupClient(connection);
            Quotes = new QuotesClient(connection);
            UserStatuses = new UserStatusesClient(connection);
            Updates = new UpdatesClient(connection);
            Recommendations = new RecommendationsClient(connection);
            ReadStatuses = new ReadStatusesClient(connection);
            OwnedBooks = new OwnedBookClient(connection);
            Comments = new CommentClient(connection);
            Topics = new TopicsClient(connection);
        }

        /// <summary>
        /// API client for the Goodreads Authors endpoint.
        /// </summary>
        public IAuthorsClient Authors { get; }

        /// <summary>
        /// API client for the Goodreads Books endpoint.
        /// </summary>
        public IBooksClient Books { get; }

        /// <summary>
        /// API client for the Goodreads Shelves endpoint.
        /// </summary>
        public IShelvesClient Shelves { get; }

        /// <summary>
        /// API client for the Goodreads Users endpoint.
        /// </summary>
        public IUsersClient Users { get; }

        /// <summary>
        /// API client for the Goodreads Reviews endpoint.
        /// </summary>
        public IReviewsClient Reviews { get; }

        /// <summary>
        /// API client for the Goodreads Series endpoint.
        /// </summary>
        public ISeriesClient Series { get; }

        /// <summary>
        /// API client for the Goodreads Author_following endpoint.
        /// </summary>
        public IAuthorsFollowingClient AuthorsFollowing { get; }

        /// <summary>
        /// API client for the Goodreads Events endpoint.
        /// </summary>
        public IEventsClient Events { get; }

        /// <summary>
        /// API client for the Goodreads user followers endpoint.
        /// </summary>
        public IFollowersClient Followers { get; }

        /// <summary>
        /// API client for the Goodreads user friends endpoint.
        /// </summary>
        public IFriendsClient Friends { get; }

        /// <summary>
        /// API client for the Goodreads notifications endpoint.
        /// </summary>
        public INotificationsClient Notifications { get; }

        /// <summary>
        /// API client for the Goodreads group endpoint.
        /// </summary>
        public IGroupClient Groups { get; }

        /// <summary>
        /// API client for the Goodreads group endpoint.
        /// </summary>
        public IQuotesClient Quotes { get; }

        /// <summary>
        /// API client for the Goodreads user statuses endpoint.
        /// </summary>
        public IUserStatusesClient UserStatuses { get; }

        /// <summary>
        /// API client for the Goodreads updates endpoint.
        /// </summary>
        public IUpdatesClient Updates { get; }

        /// <summary>
        /// API client for the Goodreads recommendations endpoint.
        /// </summary>
        public IRecommendationsClient Recommendations { get; }

        /// <summary>
        /// API client for the Goodreads read status endpoint.
        /// </summary>
        public IReadStatusesClient ReadStatuses { get; }

        /// <summary>
        /// API client for the Goodreads owned books endpoint.
        /// </summary>
        public IOwnedBookClient OwnedBooks { get; }

        /// <summary>
        /// API client for the Goodreads comments endpoint.
        /// </summary>
        public ICommentClient Comments { get; }

        /// <summary>
        /// API client for the Goodreads topics endpoint.
        /// </summary>
        public ITopicsClient Topics { get; }
    }
}
