using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for events endpoint of the Goodreads API.
    /// </summary>
    internal sealed class EventsEndpoint : Endpoint, IOAuthEventsEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventsEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public EventsEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Shows events nearby the authenticating user or
        /// you can get a list of events near a location by passing latitude/longitude, country code or postal code coordinates.
        /// </summary>
        /// <param name="latitude">A latitude.</param>
        /// <param name="longitude">A longitude.</param>
        /// <param name="countryCode">2 characters country code.</param>
        /// <param name="postalCode">A postal code.</param>
        /// <returns>A list of the Goodreads events, null if not found.</returns>
        public async Task<IReadOnlyList<GoodreadsEvent>> GetEvents(
            float? latitude,
            float? longitude,
            string countryCode,
            int? postalCode)
        {
            var parameters = new List<Parameter>();

            if (latitude.HasValue)
            {
                parameters.Add(
                    new Parameter
                    {
                        Name = "lat",
                        Value = latitude?.ToString() ?? string.Empty,
                        Type = ParameterType.QueryString
                    });
            }

            if (longitude.HasValue)
            {
                parameters.Add(
                    new Parameter
                    {
                        Name = "lng",
                        Value = longitude?.ToString() ?? string.Empty,
                        Type = ParameterType.QueryString
                    });
            }

            if (postalCode.HasValue)
            {
                parameters.Add(
                    new Parameter
                    {
                        Name = "search[postal_code]",
                        Value = postalCode?.ToString() ?? string.Empty,
                        Type = ParameterType.QueryString
                    });
            }

            if (!string.IsNullOrEmpty(countryCode))
            {
                parameters.Add(
                    new Parameter
                    {
                        Name = "search[country_code]",
                        Value = countryCode ?? string.Empty,
                        Type = ParameterType.QueryString
                    });
            }

            if (!latitude.HasValue
                && !longitude.HasValue
                && countryCode == null
                && postalCode == null)
            {
                // HACK to return the nearest events for the auth user.
                // I can't pass empty parameters because API returns a list of USA events instead of the nearest events for the auth user.
                // So I must send one incorrect parameeter. And it works correctly.
                parameters.Add(new Parameter { Name = "search[US]", Value = "US", Type = ParameterType.QueryString });
            }

            var result = await Connection.ExecuteRequest<PaginatedList<GoodreadsEvent>>("event", parameters, null, "events", Method.GET).ConfigureAwait(false);
            return result?.List;
        }
    }
}
