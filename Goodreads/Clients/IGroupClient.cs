﻿using System.Threading.Tasks;
using Goodreads.Models.Request;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Group endpoint of the Goodreads API.
    /// </summary>
    public interface IGroupClient
    {
        /// <summary>
        /// Join the current user to a given group.
        /// </summary>
        /// <param name="groupId">The Goodreads Id for the desired group.</param>
        /// <returns>True if joining succeeded, false otherwise.</returns>
        Task<bool> Join(int groupId);

        /// <summary>
        /// Get a list of groups the user specified.
        /// </summary>
        /// <param name="userId">The Goodreads Id for the desired user.</param>
        /// <param name="sort">The property to sort the groups on.</param>
        /// <returns>A paginated list of groups for the user.</returns>
        Task<PaginatedList<GroupSummary>> GetListByUser(int userId, SortGroupList? sort = null);
    }
}