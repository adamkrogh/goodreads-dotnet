using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for the Events endpoint.
    /// </summary>
    public interface IEventsEndpoint
    {
        /// <summary>
        /// Shows events nearby the authenticating user or
        /// you can get a list of events near a location by passing latitude/longitude, country code or postal code coordinates.
        /// </summary>
        /// <param name="latitude">A latitude.</param>
        /// <param name="longitude">A longitude.</param>
        /// <param name="countryCode">2 characters country code.</param>
        /// <param name="postalCode">A postal code.</param>
        /// <returns>A list of the Goodreads events, null if not found.</returns>
        Task<IReadOnlyList<GoodreadsEvent>> GetEvents(
            float? latitude = null,
            float? longitude = null,
            string countryCode = null,
            int? postalCode = null);
    }

    /// <summary>
    /// API OAuth client for the Events endpoint.
    /// </summary>
    public interface IOAuthEventsEndpoint : IEventsEndpoint
    {
    }
}
