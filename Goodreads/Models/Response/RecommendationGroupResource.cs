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
    public class RecommendationGroupResource : ApiResponse
    {
        /// <summary>
        /// The recommendation id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The user id whom book is recommended.
        /// </summary>
        public int ToUserId { get; protected set; }

        /// <summary>
        /// The user id who recommend book.
        /// </summary>
        public int FromUserId { get; protected set; }

        /// <summary>
        /// The book id which is recommended.
        /// </summary>
        public int BookId { get; protected set; }

        /// <summary>
        /// The recommendation status.
        /// </summary>
        public string Status { get; protected set; }

        /// <summary>
        /// The resource created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; protected set; }

        /// <summary>
        /// The resource updated date.
        /// </summary>
        public DateTime? UpdatedDateTime { get; protected set; }

        /// <summary>
        /// Comments count.
        /// </summary>
        public int CommentsCount { get; protected set; }

        /// <summary>
        /// The recommendation message.
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// The last comment date.
        /// </summary>
        public DateTime? LastCommentDate { get; protected set; }

        /// <summary>
        /// The recommendation request id.
        /// </summary>
        public int? RecommendationRequestId { get; protected set; }

        /// <summary>
        /// The ratings sum.
        /// </summary>
        public int RatingsSum { get; protected set; }

        /// <summary>
        /// The ratings count.
        /// </summary>
        public int RatingsCount { get; protected set; }

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
            Id = element.ElementAsInt("id");
            ToUserId = element.ElementAsInt("to_user_id");
            FromUserId = element.ElementAsInt("from_user_id");
            BookId = element.ElementAsInt("book_id");
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
