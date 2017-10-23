using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models a user recommendation as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class Recommendation : ApiResponse
    {
        /// <summary>
        /// The recommendation unique identifier.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The book which is recommended.
        /// </summary>
        public BookSummary Book { get; private set; }

        /// <summary>
        /// The recommendation created date time.
        /// </summary>
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The recommendation message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The count of likes for this recommendation.
        /// </summary>
        public int LikesCount { get; private set; }

        /// <summary>
        /// The user who send the current recommendation.
        /// </summary>
        public Actor FromUser { get; private set; }

        /// <summary>
        /// The user who receive the current recommendation.
        /// </summary>
        public Actor ToUser { get; private set; }

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
                    "Recommendation: Id: {0}, Message: {1}",
                    Id,
                    Message);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            CreatedAt = element.ElementAsDateTime("created_at");
            Message = element.ElementAsString("message");
            LikesCount = element.ElementAsInt("likes_count");

            var book = element.Element("book");
            if (book != null)
            {
                Book = new BookSummary();
                Book.Parse(book);
            }

            var fromUser = element.Element("from_user");
            if (fromUser != null)
            {
                FromUser = new Actor();
                FromUser.Parse(fromUser);
            }

            var toUser = element.Element("to_user");
            if (toUser != null)
            {
                ToUser = new Actor();
                ToUser.Parse(toUser);
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
