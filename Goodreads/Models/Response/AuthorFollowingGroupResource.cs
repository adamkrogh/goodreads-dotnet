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
    public sealed class AuthorFollowingGroupResource : ApiResponse
    {
        /// <summary>
        /// The group resource id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The user id who followed an author.
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// The following author id.
        /// </summary>
        public long AuthorId { get; private set; }

        /// <summary>
        /// The resource created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; private set; }

        /// <summary>
        /// The resource updated date.
        /// </summary>
        public DateTime? UpdatedDateTime { get; private set; }

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
            Id = element.ElementAsLong("id");
            UserId = element.ElementAsLong("user_id");
            AuthorId = element.ElementAsLong("author_id");
            CreatedDateTime = element.ElementAsDateTime("created_at");
            UpdatedDateTime = element.ElementAsDateTime("updated_at");
        }
    }
}
