using System.Threading.Tasks;
using Goodreads.Models.Response;
using Goodreads.Models.Request;

namespace Goodreads.Clients
{
	/// <summary>
    /// API client for the Topic endpoint.
    /// </summary>
    public interface ITopicsEndpoint
    {
        /// <summary>
        /// Get a list of topics in a group's folder specified either by folder id or by group id.
        /// </summary>
        /// <param name="folderId">A desire topics folder id.</param>
        /// <param name="groupId">If supplied and id is set to 0, then will return topics from the general folder for the group indicated by group_id. If id is non-zero, this param is ignored. Note: may return 404 if there are no topics in the general folder for the specified group.</param>
        /// <param name="page">The desired page from the paginated list of topics requests.</param>
        /// <param name="sort">The property to sort the topics on.</param>
        /// <param name="order">The property to order the topics on.</param>
        /// <returns>A paginated list of topics.</returns>
        Task<PaginatedList<Topic>> GetTopics(
            long folderId,
            long groupId,
            int page = 1,
            GroupFolderSort sort = GroupFolderSort.Title,
            OrderInfo order = OrderInfo.Asc);
    }

	// <summary>
    /// API OAuth client for the Shelves endpoint.
    /// </summary>
    public interface IOAuthTopicsEndpoint : ITopicsEndpoint
    {
		/// <summary>
        /// Get info about specified topic.
        /// </summary>
        /// <param name="topicId">A desire topic id.</param>
        /// <returns>A full information about topic identifier.</returns>
        Task<Topic> GetInfo(long topicId);

        /// <summary>
        /// Get a list of topics from a specified group that have comments added since the last time the user viewed the topic.
        /// User should be member of requested group.
        /// </summary>
        /// <param name="groupId">A desired group id.</param>
        /// <param name="viewed">Indicates whether to show topics user has viewed before or not.</param>
        /// <param name="page">The desired page from the paginated list of topics requests.</param>
        /// <param name="sort">The property to sort the topics on</param>
        /// <param name="order">The property to order the topics on.</param>
        /// <returns>>A paginated list of topics.</returns>
        /// <remarks>User should be member of requested group. Other way, enpoint returns 403 http error.</remarks>
        Task<PaginatedList<Topic>> GetUnreadTopics(
            long groupId,
            bool viewed = false,
            int page = 1, GroupFolderSort sort = GroupFolderSort.Title,
            OrderInfo order = OrderInfo.Asc);
    }    
}
