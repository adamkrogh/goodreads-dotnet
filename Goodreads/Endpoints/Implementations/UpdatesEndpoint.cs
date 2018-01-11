using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Helpers;
using Goodreads.Http;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Update endpoint of the Goodreads API.
    /// </summary>
    internal sealed class UpdatesEndpoint : Endpoint, IOAuthUpdatesEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatesEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public UpdatesEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Get your friend updates.
        /// </summary>
        /// <param name="type">An update type.</param>
        /// <param name="filter">An update filter.</param>
        /// <param name="maxUpdates">A maximum amount of updates.</param>
        /// <returns>Readonly friends update list.</returns>
        /// <remarks>Get the same data you see on your homepage.</remarks>
        public async Task<IReadOnlyList<Update>> GetFriendsUpdates(
            UpdateType? type,
            UpdateFilter? filter,
            int? maxUpdates)
        {
            var endpoint = @"updates/friends";

            var parameters = new List<Parameter>();

            if (type.HasValue)
            {
                var parameter = new Parameter
                {
                    Name = EnumHelpers.QueryParameterKey<UpdateType>(),
                    Value = EnumHelpers.QueryParameterValue(type.Value),
                    Type = ParameterType.QueryString
                };

                parameters.Add(parameter);
            }

            if (filter.HasValue)
            {
                var parameter = new Parameter
                {
                    Name = EnumHelpers.QueryParameterKey<UpdateFilter>(),
                    Value = EnumHelpers.QueryParameterValue(filter.Value),
                    Type = ParameterType.QueryString
                };

                parameters.Add(parameter);
            }

            if (maxUpdates.HasValue)
            {
                var parameter = new Parameter
                {
                    Name = "max_updates",
                    Value = maxUpdates.Value,
                    Type = ParameterType.QueryString
                };

                parameters.Add(parameter);
            }

            var res = await Connection.ExecuteRaw(endpoint, parameters);

            var paginated = await Connection.ExecuteRequest<PaginatedList<Update>>(endpoint, parameters, null, "updates").ConfigureAwait(false);

            return paginated?.List ?? new List<Update>();
        }
    }
}
