using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about a friend request.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class FriendRequest : ApiResponse
    {
        /// <summary>
        /// The friend request id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The resource created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; private set; }

        /// <summary>
        /// The friend message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The user who give a friend request.
        /// </summary>
        public Actor User { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}. From: {1}. Message: {2}",
                    Id,
                    User?.Id,
                    Message);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            CreatedDateTime = element.ElementAsDateTime("created_at");
            Message = element.ElementAsString("message");

            var user = element.Element("from_user");
            if (user != null)
            {
                User = new Actor();
                User.Parse(user);
            }
        }
    }
}
