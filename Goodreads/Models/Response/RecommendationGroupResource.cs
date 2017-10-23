using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about an recommendation group resource.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class RecommendationGroupResource : ApiResponse
    {
        /// <summary>
        /// The recommendation id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The user id whom book is recommended.
        /// </summary>
        public long ToUserId { get; private set; }

        /// <summary>
        /// The user id who recommend book.
        /// </summary>
        public long FromUserId { get; private set; }

        /// <summary>
        /// The book id which is recommended.
        /// </summary>
        public long BookId { get; private set; }

        /// <summary>
        /// The recommendation status.
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// The resource created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; private set; }

        /// <summary>
        /// The resource updated date.
        /// </summary>
        public DateTime? UpdatedDateTime { get; private set; }

        /// <summary>
        /// Comments count.
        /// </summary>
        public int CommentsCount { get; private set; }

        /// <summary>
        /// The recommendation message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The last comment date.
        /// </summary>
        public DateTime? LastCommentDate { get; private set; }

        /// <summary>
        /// The recommendation request id.
        /// </summary>
        public int? RecommendationRequestId { get; private set; }

        /// <summary>
        /// The ratings sum.
        /// </summary>
        public int RatingsSum { get; private set; }

        /// <summary>
        /// The ratings count.
        /// </summary>
        public int RatingsCount { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}. ToUserId: {1}. FromUserId: {2}",
                    Id,
                    ToUserId,
                    FromUserId);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            ToUserId = element.ElementAsLong("to_user_id");
            FromUserId = element.ElementAsLong("from_user_id");
            BookId = element.ElementAsLong("book_id");
            Status = element.ElementAsString("status");
            CreatedDateTime = element.ElementAsDateTime("created_at");
            UpdatedDateTime = element.ElementAsDateTime("updated_at");
            CommentsCount = element.ElementAsInt("comments_count");
            Message = element.ElementAsString("message");
            LastCommentDate = element.ElementAsDateTime("last_comment_at");
            RecommendationRequestId = element.ElementAsNullableInt("recommendation_request_id");
            RatingsSum = element.ElementAsInt("ratings_sum");
            RatingsCount = element.ElementAsInt("ratings_count");
        }
    }
}
