using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models a Review as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReviewDetails : Review
    {
        /// <summary>
        /// The user that made this review.
        /// </summary>
        public UserSummary User { get; protected set; }

        /// <summary>
        /// Status updates made while the user was reading this book.
        /// </summary>
        public IReadOnlyList<ReadStatus> ReadStatuses { get; protected set; }

        /// <summary>
        /// The paginated list of comments that have been made on this review.
        /// </summary>
        public PaginatedList<Comment> Comments { get; protected set; }

        internal new string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "ReviewDetails: Id: {0} Book.Title: {1}",
                    Id,
                    Book != null ? Book.Title : string.Empty);
            }
        }

        internal override void Parse(XElement element)
        {
            base.Parse(element);

            var userElement = element.Element("user");
            if (userElement != null)
            {
                User = new UserSummary();
                User.Parse(userElement);
            }

            ReadStatuses = element.ParseChildren<ReadStatus>("read_statuses", "read_status");

            var commentsElement = element.Element("comments");
            if (commentsElement != null)
            {
                Comments = new PaginatedList<Comment>();
                Comments.Parse(commentsElement);
            }
        }
    }
}
