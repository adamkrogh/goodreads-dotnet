using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;
using Goodreads.Models;
using Goodreads.Models.Response;

namespace Goodreads.Clients
{
    /// <summary>
    /// This class models a read status, defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReadStatus : ApiResponse
    {
        /// <summary>
        /// The read status id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The read status header.
        /// </summary>
        public string Header { get; private set; }

        /// <summary>
        /// A count of likes for the current read status.
        /// </summary>
        public int LikesCount { get; private set; }

        /// <summary>
        /// A count of comments for the current read status.
        /// </summary>
        public int CommentsCount { get; private set; }

        /// <summary>
        /// Determine whether the current read status is liked.
        /// </summary>
        public bool Liked { get; private set; }

        /// <summary>
        /// The read status created date.
        /// </summary>
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The read status updated date.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// The read status name.
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// The review identifier which relates with the current read status.
        /// </summary>
        public int ReviewId { get; private set; }

        /// <summary>
        /// The read status book.
        /// </summary>
        public BookSummary Book { get; private set; }

        /// <summary>
        /// The paginated list of comments that have been made on this status.
        /// </summary>
        public PaginatedList<Comment> Comments { get; private set; }

        /// <summary>
        /// An user who create the current read status.
        /// </summary>
        public Actor User { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}, Header: {1}",
                    Id,
                    Header);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Header = element.ElementAsString("header");
            LikesCount = element.ElementAsInt("likes_count");
            CommentsCount = element.ElementAsInt("comments_count");
            Liked = element.ElementAsBool("liked");
            CreatedAt = element.ElementAsDateTime("created_at");
            UpdatedAt = element.ElementAsDateTime("updated_at");
            Status = element.ElementAsString("status");
            ReviewId = element.ElementAsInt("review_id");

            var book = element.Element("book");
            if (book != null)
            {
                Book = new BookSummary();
                Book.Parse(book);
            }

            var comments = element.Element("comments");
            if (comments != null)
            {
                Comments = new PaginatedList<Comment>();
                Comments.Parse(comments);
            }

            var user = element.Element("user");
            if (user != null)
            {
                User = new Actor();
                User.Parse(user);
            }
        }
    }
}
