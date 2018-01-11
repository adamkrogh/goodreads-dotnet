using Goodreads.Clients;

namespace Goodreads
{
    /// <summary>
    /// The API interface for accessing the OAuth Goodreads API.
    /// </summary>
    public interface IOAuthGoodreadsClient : IApiCredentialsInfoManager
    {
        /// <summary>
        /// API OAuth client for the Goodreads Authors endpoint.
        /// </summary>
        IOAuthAuthorsEndpoint Authors { get; }

        /// <summary>
        /// API OAuth client for the Goodreads Authors endpoint.
        /// </summary>
        IOAuthAuthorsFollowingEndpoint AuthorsFollowing { get; }

        /// <summary>
        /// API OAuth client for the Goodreads Books endpoint.
        /// </summary>
        IOAuthBooksEndpoint Books { get; }

        /// <summary>
        /// API OAuth client for the Goodreads comments endpoint.
        /// </summary>
        IOAuthCommentsEndpoint Comments { get; }

        /// <summary>
        /// API OAuth client for the Goodreads events endpoint.
        /// </summary>
        IOAuthEventsEndpoint Events { get; }

        /// <summary>
        /// API OAuth client for the Goodreads followers endpoint.
        /// </summary>
        IOAuthFollowersEndpoint Followers { get; }

        /// <summary>
        /// API OAuth client for the Goodreads Shelves endpoint.
        /// </summary>
        IOAuthFriendsEndpoint Friends { get; }

        /// <summary>
        /// API OAuth client for the Goodreads group endpoint.
        /// </summary>
        IOAuthGroupsEndpoint Groups { get; }

        /// <summary>
        /// API OAuth client for the Goodreads notifications endpoint.
        /// </summary>
        IOAuthNotificationsEndpoint Notifications { get; }

        /// <summary>
        /// API OAuth client for the Goodreads owned books endpoint.
        /// </summary>
        IOAuthOwnedBooksEndpoint OwnedBooks { get; }

        /// <summary>
        /// API OAuth client for the Goodreads quotes endpoint.
        /// </summary>
        IOAuthQuotesEndpoint Quotes { get; }

        /// <summary>
        /// API OAuth client for the Goodreads read status endpoint.
        /// </summary>
        IOAuthReadStatusesEndpoint ReadStatuses { get; }

        /// <summary>
        /// API OAuth client for the Goodreads recommendations endpoint.
        /// </summary>
        IOAuthRecommendationsEndpoint Recommendations { get; }

        /// <summary>
        /// API OAuth client for the Goodreads Reviews endpoint.
        /// </summary>
        IOAuthReviewsEndpoint Reviews { get; }

        /// <summary>
        /// API OAuth client for the Goodreads Series endpoint.
        /// </summary>
        IOAuthSeriesEndpoint Series { get; }

        /// <summary>
        /// API OAuth client for the Goodreads Shelves endpoint.
        /// </summary>
        IOAuthShelvesEndpoint Shelves { get; }

        /// <summary>
        /// API OAuth client for the Goodreads topics endpoint.
        /// </summary>
        IOAuthTopicsEndpoint Topics { get; }

        /// <summary>
        /// API OAuth client for the Goodreads updates endpoint.
        /// </summary>
        IOAuthUpdatesEndpoint Updates { get; }

        /// <summary>
        /// API OAuth client for the Goodreads Users endpoint.
        /// </summary>
        IOAuthUsersEndpoint Users { get; }

        /// <summary>
        /// API OAuth client for the Goodreads user statuses endpoint.
        /// </summary>
        IOAuthUserStatusesEndpoint UserStatuses { get; }
    }
}
