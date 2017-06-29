using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Goodreads.Http;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// API client for user friends endpoint.
    /// </summary>
    public class FriendsClient : IFriendsClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendsClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public FriendsClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Sends a friend request to a user.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <returns>True if adding succeeded, false otherwise.</returns>
        public async Task<bool> AddFriend(int userId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = userId, Type = ParameterType.QueryString }
            };

            var response = await Connection.ExecuteRaw("friend/add_as_friend", parameters, Method.POST);

            return response.StatusCode == HttpStatusCode.Created;
        }
    }
}
