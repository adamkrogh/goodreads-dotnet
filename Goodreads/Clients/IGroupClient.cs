using System.Threading.Tasks;

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
    }
}
