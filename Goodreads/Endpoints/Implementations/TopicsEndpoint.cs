using Goodreads.Helpers;
using Goodreads.Http;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goodreads.Clients
{
    internal sealed class TopicsEndpoint : Endpoint, IOAuthTopicsEndpoint
    {
        public TopicsEndpoint(IConnection connection)
            : base(connection)
        {
        }

        public async Task<Topic> GetInfo(long topicId)
        {
            var endpoint = $"topic/show?id={topicId}";
            return await Connection.ExecuteRequest<Topic>(endpoint, null, null, "topic").ConfigureAwait(false);
        }

        public async Task<PaginatedList<Topic>> GetTopics(
            long folderId,
            long groupId,
            int page = 1,
            GroupFolderSort sort = GroupFolderSort.Title,
            OrderInfo order = OrderInfo.Asc)
        {
            var endpoint = $"topic/group_folder/{folderId}";

            var parameters = new[]
            {
                 new Parameter { Name = "group_id", Value = groupId, Type = ParameterType.QueryString },
                 new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString },
                 new Parameter
                 {
                     Name = EnumHelpers.QueryParameterKey<GroupFolderSort>(),
                     Value = EnumHelpers.QueryParameterValue(sort),
                     Type = ParameterType.QueryString
                 },
                 new Parameter
                 {
                     Name = EnumHelpers.QueryParameterKey<OrderInfo>(),
                     Value = EnumHelpers.QueryParameterValue(order),
                     Type = ParameterType.QueryString
                 }
            };

            return await Connection.ExecuteRequest<PaginatedList<Topic>>(endpoint, parameters, null, "group_folder/topics").ConfigureAwait(false);
        }

        public async Task<PaginatedList<Topic>> GetUnreadTopics(
            long groupId,
            bool viewed = false,
            int page = 1,
            GroupFolderSort sort = GroupFolderSort.Title,
            OrderInfo order = OrderInfo.Asc)
        {
            var endpoint = $"topic/unread_group/{groupId}";

            var parameters = new List<Parameter>
            {                 
                 new Parameter { Name = "page", Value = page, Type = ParameterType.QueryString },
                 new Parameter
                 {
                     Name = EnumHelpers.QueryParameterKey<GroupFolderSort>(),
                     Value = EnumHelpers.QueryParameterValue(sort),
                     Type = ParameterType.QueryString
                 },
                 new Parameter
                 {
                     Name = EnumHelpers.QueryParameterKey<OrderInfo>(),
                     Value = EnumHelpers.QueryParameterValue(order),
                     Type = ParameterType.QueryString
                 }
            };

            if (viewed)
            {
                parameters.Add(new Parameter { Name = "viewed", Value = viewed, Type = ParameterType.QueryString });
            }

            var q = await Connection.ExecuteRaw(endpoint, parameters);

            return await Connection.ExecuteRequest<PaginatedList<Topic>>(endpoint, parameters, null, "group_folder/topics").ConfigureAwait(false);
        }
    }
}
