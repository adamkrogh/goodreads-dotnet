using Goodreads.Helpers;
using Goodreads.Http;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Users endpoint of the Goodreads API.
    /// </summary>
    internal sealed class UsersEndpoint : Endpoint, IOAuthUsersEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public UsersEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Gets the public information for a Goodreads user.
        /// </summary>
        /// <param name="userId">The Goodreads user id of the user to fetch.</param>
        /// <returns>Information about the desired user.</returns>
        public async Task<User> GetByUserId(long userId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = userId, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<User>("user/show", parameters, null, "user").ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the public information for a Goodreads user by username.
        /// Note that usernames are optional in Goodreads.
        /// </summary>
        /// <param name="username">The Goodreads username of the user to fetch.</param>
        /// <returns>Information about the desired user.</returns>
        public async Task<User> GetByUsername(string username)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "username", Value = username, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<User>("user/show", parameters, null, "user").ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a paginated list of friends for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <param name="sort">The sort order of the paginated list.</param>
        /// <returns>A paginated list of the user summary information for their friends.</returns>
        public async Task<PaginatedList<UserSummary>> GetListOfFriends(long userId, int page, SortFriendsList sort)
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

            return await Connection.ExecuteRequest<PaginatedList<UserSummary>>("friend/user", parameters, null, "friends").ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the Goodreads user id of the authenticated connection.
        /// If the client isn't using OAuth, this returns null.
        /// </summary>
        /// <returns>The user id of the authenticated user. Null if just using the public API.</returns>
        public async Task<long> GetAuthenticatedUserId()
        {
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
                            return long.Parse(attribute.Value);
                        }
                    }
                }
            }

            throw new Exception();
        }

        /// <summary>
        /// Get an people the given user is following.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>People the given user is following.</returns>
        public async Task<PaginatedList<UserFollowing>> GetUserFollowing(long userId, int page)
        {
            var endpoint = $"user/{userId}/following";

            var parameters = new List<Parameter>()
            {
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString },
            };

            return await Connection.ExecuteRequest<PaginatedList<UserFollowing>>(endpoint, parameters, null, "following").ConfigureAwait(false);
        }

        /// <summary>
        /// Get given user's followers.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <returns>The specified user's followers.</returns>
        public async Task<PaginatedList<UserFollowers>> GetUsersFollowers(long userId, int page)
        {
            var endpoint = $"user/{userId}/followers";

            var parameters = new List<Parameter>()
            {
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString },
            };

            return await Connection.ExecuteRequest<PaginatedList<UserFollowers>>(endpoint, parameters, null, "followers").ConfigureAwait(false);
        }

        /// <summary>
        /// Get stats comparing your books to another member's.
        /// </summary>
        /// <param name="userId">A desired user if to ompare.</param>
        /// <returns>A compare books result.</returns>
        public async Task<CompareBooksResult> CompareUserBooks(long userId)
        {
            var endpoint = $"user/compare/{userId}";
            return await Connection.ExecuteRequest<CompareBooksResult>(endpoint, new List<Parameter>(), null, "compare").ConfigureAwait(false);
        }
    }
}
