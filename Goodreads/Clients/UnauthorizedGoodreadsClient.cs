using Goodreads.Clients;
using Goodreads.Http;
using System.Threading.Tasks;

namespace Goodreads
{
    /// <summary>
    /// The unauthorized client API class for accessing the Goodreads API.
    /// </summary>
    public sealed class UnauthorizedGoodreadsClient : GoodreadsClient, IGoodreadsClient
    {
        public UnauthorizedGoodreadsClient(string apiKey, string apiSecret, string accessToken, string accessSecret)
            : base(apiKey, apiSecret, accessToken, accessSecret)
        {
            Authors = new AuthorsEnpoint(_connection);
            Books = new BooksEndpoint(_connection);
            Shelves = new ShelvesEndpoint(_connection);
            Users = new UsersEndpoint(_connection);
            Reviews = new ReviewsEndpoint(_connection);
            Series = new SeriesEndpoint(_connection);
            Events = new EventsEndpoint(_connection);
            Groups = new GroupsEndpoint(_connection);
            UserStatuses = new UserStatusesEndpoint(_connection);
            ReadStatuses = new ReadStatusesEndpoint(_connection);
            Comments = new CommentsEndpoint(_connection);
            Topics = new TopicsEndpoint(_connection);
        }

        public IAuthorsEndpoint Authors { get; }

        public IBooksEndpoint Books { get; }

        public IShelvesEndpoint Shelves { get; }

        public IUsersEndpoint Users { get; }

        public IReviewsEndpoint Reviews { get; }

        public ISeriesEndpoint Series { get; }

        public IEventsEndpoint Events { get; }

        public IGroupsEndpoint Groups { get; }

        public IUserStatusesEndpoint UserStatuses { get; }

        public IReadStatusesEndpoint ReadStatuses { get; }

        public ICommentsEndpoint Comments { get; }

        public ITopicsEndpoint Topics { get; }

        public async Task<OAuthRequestToken> AskCredentials(string callbackUrl)
        {
            return await _connection.GetRequestToken(callbackUrl).ConfigureAwait(false);
        }

        public async Task<OAuthAccessToken> GetAccessToken(OAuthRequestToken token)
        {
            return await _connection.GetAccessToken(token).ConfigureAwait(false);
        }

        public async Task<OAuthAccessToken> GetAccessToken(string token, string secret)
        {
            var oAuthRequestToken = new OAuthRequestToken(token, secret);
            return await GetAccessToken(oAuthRequestToken).ConfigureAwait(false);
        }
    }
}
