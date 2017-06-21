using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about an Author following.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuthorFollowing : ApiResponse
    {
        /// <summary>
        /// The Goodreads Author_following Id.
        /// </summary>
        public int Id { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "AuthorFollowingId: Id: {0}", Id);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
        }
    }
}
