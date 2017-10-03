using System.Collections.Generic;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Request;
using RestSharp;
using System.Net;
using Goodreads.Helpers;

namespace Goodreads.Clients
{
    public sealed class CommentClient : ICommentClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public CommentClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Create a new comment.
        /// </summary>
        /// <param name="resourceId">Id of resource given as resourceType param.</param>
        /// <param name="type">A resource type.</param>
        /// <param name="comment">A comment value.</param>
        /// <returns>True if creation is successed. otherwise false.</returns>
        public async Task<bool> Create(int resourceId, ResourceType type, string comment)
        {
            var endpoint = @"comment";

            var parameters = new List<Parameter>
            {
                new Parameter { Name = "type", Value = EnumHelpers.QueryParameterValue(type), Type = ParameterType.QueryString },
                new Parameter { Name = "id", Value = resourceId, Type = ParameterType.QueryString },
                new Parameter { Name = "comment[body]", Value = comment, Type = ParameterType.QueryString }
            };

            var result = await Connection.ExecuteRaw(endpoint, parameters, Method.POST);

            return result.StatusCode == HttpStatusCode.Created;
        }
    }
}
