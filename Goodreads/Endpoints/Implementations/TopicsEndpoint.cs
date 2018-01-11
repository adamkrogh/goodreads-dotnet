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

            return await Connection.ExecuteRequest<PaginatedList<Topic>>(endpoint, parameters, null, "group_folder/topics").ConfigureAwait(false);
        }

        public async Task<Topic> CreateTopic(
            TopicSubjectType type,
            long subjectId,
            long? folderId,
            string title,
            bool isQuestion,
            string comment,
            bool addToUpdateFeed,
            bool needDigest)
        {
            var endpoint = $"topic";

            var parameters = new List<Parameter>
            {
                 new Parameter
                 {
                     Name = EnumHelpers.QueryParameterKey<TopicSubjectType>(),
                     Value = EnumHelpers.QueryParameterValue(type),
                     Type = ParameterType.QueryString
                 },
                 new Parameter
                 {
                     Name = "topic[subject_id]",
                     Value = subjectId,
                     Type = ParameterType.QueryString
                 },
                 new Parameter
                 {
                     Name = "topic[title]",
                     Value = title,
                     Type = ParameterType.QueryString
                 },
                 new Parameter
                 {
                     Name = "topic[question_flag]",
                     Value = isQuestion ? "1" : "0",
                     Type = ParameterType.QueryString
                 },
                 new Parameter
                 {
                     Name = "comment[body_usertext]",
                     Value = comment,
                     Type = ParameterType.QueryString
                 }
            };

            if (folderId.HasValue)
            {
                parameters.Add(new Parameter { Name = "topic[folder_id]", Value = folderId.Value, Type = ParameterType.QueryString });
            }

            if (addToUpdateFeed)
            {
                parameters.Add(new Parameter { Name = "update_feed", Value = "on", Type = ParameterType.QueryString });
            }

            if (needDigest)
            {
                parameters.Add(new Parameter { Name = "digest", Value = "on", Type = ParameterType.QueryString });
            }

            return await Connection.ExecuteRequest<Topic>(endpoint, parameters, null, "topic", Method.POST);
        }
    }
}
