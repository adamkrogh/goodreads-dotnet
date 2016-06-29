using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// Gets a paginated list of friends for the given Goodreads user id.
        /// </summary>
        /// <param name="userId">The Goodreads user id.</param>
        /// <param name="page">The current page of the paginated list.</param>
        /// <param name="sort">The sort order of the paginated list.</param>
        /// <returns>A paginated list of the user's friends.</returns>
        public Task<PaginatedList<User>> GetListOfFriends(int userId, int page = 1, SortFriendsList sort = SortFriendsList.FirstName)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = userId, Type = ParameterType.QueryString },
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
                //// TODO: implement the sort query parameter from the enum
                //// new Parameter { Name = "sort", Value = sort, Type = ParameterType.QueryString }
            };

            return Connection.ExecuteRequest<PaginatedList<User>>("friend/user", parameters, null, "friends");
        }
    }
}
