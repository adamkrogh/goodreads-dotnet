using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the user followers endpoint of the Goodreads API.
    /// </summary>
    internal sealed class FollowersClient : EndpointClient, IFollowersClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FollowersClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public FollowersClient(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Follow an user.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <returns>A Goodreads user following model.</returns>
        Task<UserFollowingSummary> IFollowersClient.Follow(int userId)
        {
            var endpoint = string.Format(CultureInfo.InvariantCulture, "user/{0}/followers", userId);
            return Connection.ExecuteRequest<UserFollowingSummary>(endpoint, null, null, "user-following", Method.POST);
        }

        /// <summary>
        /// Unfollow an user.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <returns>True if the unfollow succeeded, false otherwise.</returns>
        async Task<bool> IFollowersClient.Unfollow(int userId)
        {
            var endpoint = string.Format(CultureInfo.InvariantCulture, "user/{0}/followers/stop_following", userId);
            var response = await Connection.ExecuteRaw(endpoint, null, Method.DELETE).ConfigureAwait(false);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
