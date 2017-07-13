using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Goodreads.Helpers;
using Goodreads.Http;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Group endpoint of the Goodreads API.
    /// </summary>
    public class GroupClient : IGroupClient
    {
        private readonly IConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupClient"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public GroupClient(IConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Join the current user to a given group.
        /// </summary>
        /// <param name="groupId">The Goodreads Id for the desired group.</param>
        /// <returns>True if joining succeeded, false otherwise.</returns>
        public async Task<bool> Join(int groupId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = groupId, Type = ParameterType.QueryString }
            };

            var response = await Connection.ExecuteRaw("group/join", parameters, Method.POST);

            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Get a list of groups the user specified.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <param name="sort">The property to sort the groups on.</param>
        /// <returns>A paginated list of groups for the user.</returns>
        public async Task<PaginatedList<GroupSummary>> GetListByUser(int userId, SortGroupList? sort = null)
        {
            var endpoint = $"group/list/{userId}";

            var parameters = new List<Parameter>();

            if (sort.HasValue)
            {
                var parameter = new Parameter
                {
                    Name = EnumHelpers.QueryParameterKey<SortGroupList>(),
                    Value = EnumHelpers.QueryParameterValue(sort.Value),
                    Type = ParameterType.QueryString
                };

                parameters.Add(parameter);
            }

            return await Connection.ExecuteRequest<PaginatedList<GroupSummary>>(endpoint, parameters, null, "groups/list", Method.GET);
        }

        /// <summary>
        /// Search group by specified titles and descriptions.
        /// </summary>
        /// <param name="search">A search string criteria.</param>
        /// <param name="page">A page number.</param>
        /// <returns>A paginated list of groups for the user.</returns>
        public async Task<PaginatedList<GroupSummary>> GetGroups(string search, int page = 1)
        {
            var parameters = new List<Parameter>()
            {
                new Parameter { Name = "q", Value = search, Type = ParameterType.QueryString },
                new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<PaginatedList<GroupSummary>>("group/search", parameters, null, "groups/list");
        }

        /// <summary>
        /// Get info about a group by specified id.
        /// </summary>
        /// <param name="groupId">The Goodreads Group id.</param>
        /// <param name="sort">The property to sort the group info on.</param>
        /// <param name="order">The property to order the group info on.</param>
        /// <returns>The Goodreads Group model.</returns>
        public async Task<Group> GetInfo(int groupId, SortGroupInfo? sort = null, OrderGroupInfo? order = null)
        {
            var endpoint = $"group/show/{groupId}";

            var parameters = new List<Parameter>();

            if (sort.HasValue)
            {
                var parameter = new Parameter
                {
                    Name = EnumHelpers.QueryParameterKey<SortGroupInfo>(),
                    Value = EnumHelpers.QueryParameterValue(sort.Value),
                    Type = ParameterType.QueryString
                };
                parameters.Add(parameter);
            }

            if (order.HasValue)
            {
                var parameter = new Parameter
                {
                    Name = EnumHelpers.QueryParameterKey<OrderGroupInfo>(),
                    Value = EnumHelpers.QueryParameterValue(order.Value),
                    Type = ParameterType.QueryString
                };
                parameters.Add(parameter);
            }

            return await Connection.ExecuteRequest<Group>(endpoint, parameters, null, "group");
        }
    }
}
