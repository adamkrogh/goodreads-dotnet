using Goodreads.Clients;

namespace Goodreads
{
    /// <summary>
    /// The authorized client API class for accessing the OAuth Goodreads API.
    /// </summary>
    public sealed class OAuthGoodreadsClient : GoodreadsClient, IOAuthGoodreadsClient
    {
        public OAuthGoodreadsClient(string apiKey, string apiSecret, string accessToken, string accessSecret)
            : base(apiKey, apiSecret, accessToken, accessSecret)
        {
            Authors = new AuthorsEnpoint(_connection);
            AuthorsFollowing = new AuthorsFollowingEndpoint(_connection);
            Books = new BooksEndpoint(_connection);
            Comments = new CommentsEndpoint(_connection);
            Events = new EventsEndpoint(_connection);
            Followers = new FollowersEndpoint(_connection);
            Friends = new FriendsEndpoint(_connection);
            Groups = new GroupsEndpoint(_connection);
            Notifications = new NotificationsEndpoint(_connection);
            OwnedBooks = new OwnedBooksEndpoint(_connection);
            Quotes = new QuotesEndpoint(_connection);
            ReadStatuses = new ReadStatusesEndpoint(_connection);
            Recommendations = new RecommendationsEndpoint(_connection);
            Reviews = new ReviewsEndpoint(_connection);
            Series = new SeriesEndpoint(_connection);
            Shelves = new ShelvesEndpoint(_connection);
            Topics = new TopicsEndpoint(_connection);
            Updates = new UpdatesEndpoint(_connection);
            Users = new UsersEndpoint(_connection);
            UserStatuses = new UserStatusesEndpoint(_connection);
        }

        public IOAuthAuthorsEndpoint Authors { get; }

        public IOAuthAuthorsFollowingEndpoint AuthorsFollowing { get; }

        public IOAuthBooksEndpoint Books { get; }

        public IOAuthCommentsEndpoint Comments { get; }

        public IOAuthEventsEndpoint Events { get; }

        public IOAuthFollowersEndpoint Followers { get; }

        public IOAuthFriendsEndpoint Friends { get; }

        public IOAuthGroupsEndpoint Groups { get; }

        public IOAuthNotificationsEndpoint Notifications { get; }

        public IOAuthOwnedBooksEndpoint OwnedBooks { get; }

        public IOAuthQuotesEndpoint Quotes { get; }

        public IOAuthReadStatusesEndpoint ReadStatuses { get; }

        public IOAuthRecommendationsEndpoint Recommendations { get; }

        public IOAuthReviewsEndpoint Reviews { get; }

        public IOAuthSeriesEndpoint Series { get; }

        public IOAuthShelvesEndpoint Shelves { get; }

        public IOAuthTopicsEndpoint Topics { get; }

        public IOAuthUpdatesEndpoint Updates { get; }

        public IOAuthUsersEndpoint Users { get; }

        public IOAuthUserStatusesEndpoint UserStatuses { get; }
    }
}
