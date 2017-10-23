using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models a status update while reading, as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReadStatus : ApiResponse
    {
        /// <summary>
        /// The id of this status update.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The read status header.
        /// </summary>
        public string Header { get; private set; }

        /// <summary>
        /// The status/description of this update.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The date and time this status was updated at.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The number of likes this status update has.
        /// </summary>
        public int RatingsCount { get; set; }

        /// <summary>
        /// The number of comments this status update has.
        /// </summary>
        public int CommentsCount { get; set; }

        /// <summary>
        /// THe old status of this update.
        /// </summary>
        public string OldStatus { get; set; }

        /// <summary>
        /// The user id who made update.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        ///  The review id.
        /// </summary>
        public long? ReviewId { get; set; }

        /// <summary>
        /// The review.
        /// </summary>
        public Review Review { get; private set; }

        /// <summary>
        /// A count of likes for the current read status.
        /// </summary>
        public int LikesCount { get; private set; }

        /// <summary>
        /// Determine whether the current read status is liked.
        /// </summary>
        public bool Liked { get; private set; }

        /// <summary>
        /// The read status created date.
        /// </summary>
        public DateTime? CreatedAt { get; private set; }

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
            Id = element.ElementAsLong("id");
            Header = element.ElementAsString("header");
            Status = element.ElementAsString("status");
            UpdatedAt = element.ElementAsDateTime("updated_at");
            RatingsCount = element.ElementAsInt("ratings_count");
            CommentsCount = element.ElementAsInt("comments_count");
            OldStatus = element.ElementAsString("old_status");
            UserId = element.ElementAsNullableLong("user_id");
            LikesCount = element.ElementAsInt("likes_count");
            Liked = element.ElementAsBool("liked");
            CreatedAt = element.ElementAsDateTime("created_at");
            ReviewId = element.ElementAsNullableLong("review_id");

            var review = element.Element("review");
            if (review != null)
            {
                Review = new Review();
                Review.Parse(review);
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

            var user = element.Element("user");
            if (user != null)
            {
                User = new Actor();
                User.Parse(user);
            }
        }
    }
}
