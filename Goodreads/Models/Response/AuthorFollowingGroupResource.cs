using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about an Author following group resource.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuthorFollowingGroupResource : ApiResponse
    {
        /// <summary>
        /// The group resource id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The user id who followed an author.
        /// </summary>
        public int UserId { get; protected set; }

        /// <summary>
        /// The following author id.
        /// </summary>
        public int AuthorId { get; protected set; }

        /// <summary>
        /// The resource created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; protected set; }

        /// <summary>
        /// The resource updated date.
        /// </summary>
        public DateTime? UpdatedDateTime { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}. UserId: {1}. Author: {2}",
                    Id,
                    UserId,
                    AuthorId);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            UserId = element.ElementAsInt("user_id");
            AuthorId = element.ElementAsInt("author_id");
            CreatedDateTime = element.ElementAsDateTime("created_at");
            UpdatedDateTime = element.ElementAsDateTime("updated_at");
        }
    }
}
