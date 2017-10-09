using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Goodreads.Helpers;
using Goodreads.Http;
using Goodreads.Models.Request;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for rating endpoint.
    /// </summary>
    internal sealed class RatingClient : EndpointClient, IRatingClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public RatingClient(IConnection connection)
            : base(connection, @"rating")
        {
        }

        async Task<bool> IRatingClient.LikeResource(int resourceId, ResourceType type)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "rating[rating]", Value = 1, Type = ParameterType.QueryString },
                new Parameter { Name = "rating[resource_id]", Value = resourceId, Type = ParameterType.QueryString },
                new Parameter { Name = "rating[resource_type]",  Value = EnumHelpers.QueryParameterValue(type), Type = ParameterType.QueryString },
            };

            var result = await Connection.ExecuteRaw(Endpoint, parameters, Method.POST);

            return result.StatusCode == HttpStatusCode.Created;
        }
    }
}
