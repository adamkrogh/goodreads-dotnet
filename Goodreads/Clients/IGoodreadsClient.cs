using Goodreads.Clients;

namespace Goodreads
{
    /// <summary>
    /// The API interface for accessing the Goodreads API.
    /// </summary>
    public interface IGoodreadsClient : IAuthManager
    {
        /// <summary>
        /// API client for the Goodreads Authors endpoint.
        /// </summary>
        IAuthorsEndpoint Authors { get; }

        /// <summary>
        /// API client for the Goodreads Books endpoint.
        /// </summary>
        IBooksEndpoint Books { get; }

        /// <summary>
        /// API client for the Goodreads Shelves endpoint.
        /// </summary>
        IShelvesEndpoint Shelves { get; }

        /// <summary>
        /// API client for the Goodreads Users endpoint.
        /// </summary>
        IUsersEndpoint Users { get; }

        /// <summary>
        /// API client for the Goodreads Reviews endpoint.
        /// </summary>
        IReviewsEndpoint Reviews { get; }

        /// <summary>
        /// API client for the Goodreads Series endpoint.
        /// </summary>
        ISeriesEndpoint Series { get; }

        /// <summary>
        /// API client for the Goodreads Events endpoint.
        /// </summary>
        IEventsEndpoint Events { get; }

        /// <summary>
        /// API client for the Goodreads group endpoint.
        /// </summary>
        IGroupsEndpoint Groups { get; }

        /// <summary>
        /// API client for the Goodreads user statuses endpoint.
        /// </summary>
        IUserStatusesEndpoint UserStatuses { get; }

        /// <summary>
        /// API client for the Goodreads read status endpoint.
        /// </summary>
        IReadStatusesEndpoint ReadStatuses { get; }

        /// <summary>
        /// API client for the Goodreads comments endpoint.
        /// </summary>
        ICommentsEndpoint Comments { get; }

        /// <summary>
        /// API client for the Goodreads topics endpoint.
        /// </summary>
        ITopicsEndpoint Topics { get; }
    }
}
