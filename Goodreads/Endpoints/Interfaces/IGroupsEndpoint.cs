using System.Threading.Tasks;
using Goodreads.Models.Request;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Group endpoint of the Goodreads API.
    /// </summary>
    public interface IGroupsEndpoint
    {
        /// <summary>
        /// Get a list of groups the user specified.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <param name="sort">The property to sort the groups on.</param>
        /// <returns>A paginated list of groups for the user.</returns>
        Task<PaginatedList<GroupSummary>> GetListByUser(long userId, SortGroupList? sort = null);

        /// <summary>
        /// Search group by specified titles and descriptions.
        /// </summary>
        /// <param name="search">A search string criteria.</param>
        /// <param name="page">A page number.</param>
        /// <returns>A paginated list of groups for the user.</returns>
        Task<PaginatedList<GroupSummary>> GetGroups(string search, int page = 1);

        /// <summary>
        /// Get info about a group by specified id.
        /// </summary>
        /// <param name="groupId">The Goodreads Group id.</param>
        /// <param name="sort">The property to sort the group info on.</param>
        /// <param name="order">The property to order the group info on.</param>
        /// <returns>The Goodreads Group model.</returns>
        Task<Group> GetInfo(long groupId, SortGroupInfo? sort = null, OrderInfo? order = null);

        /// <summary>
        /// Get list of members of the specified group.
        /// </summary>
        /// <param name="groupId">The Goodreads Group id.</param>
        /// <param name="names">List of names to search.</param>
        /// <param name="page">A page number.</param>
        /// <param name="sort">The property to sort the group member on.</param>
        /// <returns>A paginated list of groups members.</returns>
        Task<PaginatedList<GroupUser>> GetMembers(
            long groupId,
            string[] names = null,
            int page = 1,
            SortGroupMember sort = SortGroupMember.FristName);
    }

    /// <summary>
    /// The OAuth client class for the Group endpoint of the Goodreads API.
    /// </summary>
    public interface IOAuthGroupsEndpoint : IGroupsEndpoint
    {
        /// <summary>
        /// Join the current user to a given group.
        /// </summary>
        /// <param name="groupId">The Goodreads Id for the desired group.</param>
        /// <returns>True if joining succeeded, false otherwise.</returns>
        Task<bool> Join(long groupId);
    }
}
