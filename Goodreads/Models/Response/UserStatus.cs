using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;
using Goodreads.Models;
using Goodreads.Models.Response;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents information about an user status.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class UserStatus : ApiResponse
    {
        /// <summary>
        /// The user status id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The user status header.
        /// </summary>
        public string Header { get; private set; }

        /// <summary>
        /// The user status body.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// The user status created date.
        /// </summary>
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The user status updated date.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// The likes count.
        /// </summary>
        public int LikesCount { get; private set; }

        /// <summary>
        /// The comment count.
        /// </summary>
        public int CommentsCount { get; private set; }

        /// <summary>
        /// Determine whether user status is liked.
        /// </summary>
        public bool Liked { get; private set; }

        /// <summary>
        /// The current read page.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// The current read percent.
        /// </summary>
        public int Percent { get; private set; }

        /// <summary>
        /// Work id.
        /// </summary>
        public long WorkId { get; private set; }

        /// <summary>
        /// The user who is a status author.
        /// </summary>
        public Actor User { get; private set; }

        /// <summary>
        /// A brief information about status book.
        /// </summary>
        public BookSummary Book { get; private set; }

        /// <summary>
        /// The paginated list of comments that have been made on this status.
        /// </summary>
        public PaginatedList<Comment> Comments { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "UserStatus: Id: {0}, Header: {1}",
                    Id,
                    Header);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Header = element.ElementAsString("header");
            Body = element.ElementAsString("body");
            CreatedAt = element.ElementAsDateTime("created_at");
            UpdatedAt = element.ElementAsDateTime("updated_at");
            LikesCount = element.ElementAsInt("likes_count");
            CommentsCount = element.ElementAsInt("comments_count");
            Liked = element.ElementAsBool("liked");
            Page = element.ElementAsInt("page");
            Percent = element.ElementAsInt("percent");
            WorkId = element.ElementAsLong("work_id");

            var actor = element.Element("user");
            if (actor != null)
            {
                User = new Actor();
                User.Parse(actor);
            }

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
        }
    }
}
