using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Goodreads.Helpers;
using Goodreads.Http;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Users endpoint of the Goodreads API.
    /// </summary>
    public class UsersClient : IUsersClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public UsersClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Gets the public information for a Goodreads user.
        /// </summary>
        /// <param name="userId">The Goodreads user id of the user to fetch.</param>
        /// <returns>Information about the desired user.</returns>
        public Task<User> GetByUserId(int userId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = userId, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<User>("user/show", parameters, null, "user");
        }

        /// <summary>
        /// Gets the public information for a Goodreads user by username.
        /// Note that usernames are optional in Goodreads.
        /// </summary>
        /// <param name="username">The Goodreads username of the user to fetch.</param>
        /// <returns>Information about the desired user.</returns>
        public Task<User> GetByUsername(string username)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "username", Value = username, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<User>("user/show", parameters, null, "user");
        }

        /// <summary>
        /// Gets a paginated list of friends for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <param name="sort">The sort order of the paginated list.</param>
        /// <returns>A paginated list of the user summary information for their friends.</returns>
        public Task<PaginatedList<UserSummary>> GetListOfFriends(int userId, int page = 1, SortFriendsList sort = SortFriendsList.FirstName)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = userId, Type = ParameterType.QueryString },
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString },
                new Parameter
                {
                    Name = EnumHelpers.QueryParameterKey<SortFriendsList>(),
                    Value = EnumHelpers.QueryParameterValue(sort),
                    Type = ParameterType.QueryString
                }
            };

            return Connection.ExecuteRequest<PaginatedList<UserSummary>>("friend/user", parameters, null, "friends");
        }

        /// <summary>
        /// Gets the Goodreads user id of the authenticated connection.
        /// If the client isn't using OAuth, this returns null.
        /// </summary>
        /// <returns>The user id of the authenticated user. Null if just using the public API.</returns>
        public async Task<int?> GetAuthenticatedUserId()
        {
            if (!Connection.IsAuthenticated)
            {
                return null;
            }

            // This response is simple enough that we just parse it here without creating another model
            var response = await Connection.ExecuteRaw("api/auth_user", null).ConfigureAwait(false);
            if (response != null && (int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
            {
                var content = response.Content;
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var document = XDocument.Parse(content);
                    var userElement = document.XPathSelectElement("GoodreadsResponse/user");
                    if (userElement != null)
                    {
                        var attribute = userElement.Attribute("id");
                        if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Value))
                        {
                            return int.Parse(attribute.Value);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get an people the given user is following.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>People the given user is following.</returns>
        public async Task<PaginatedList<UserFollowing>> GetUserFollowing(int userId, int page = 1)
        {
            var endpoint = $"user/{userId}/following";

            var parameters = new List<Parameter>()
            {
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString },
            };

            return await Connection.ExecuteRequest<PaginatedList<UserFollowing>>(endpoint, parameters, null, "following");
        }

        /// <summary>
        /// Get given user's followers.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>The specified user's followers.</returns>
        public async Task<PaginatedList<UserFollowers>> GetUsersFollowers(int userId, int page = 1)
        {
            var endpoint = $"user/{userId}/followers";

            var parameters = new List<Parameter>()
            {
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString },
            };

            var q = await Connection.ExecuteRaw(endpoint, parameters);

            return await Connection.ExecuteRequest<PaginatedList<UserFollowers>>(endpoint, parameters, null, "followers");
        }
    }
}
