using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about an User following status.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class UserFollowingStatus : ApiResponse
    {
        /// <summary>
        /// The user status id.
        /// </summary>
        public int Id { get; private set; }

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
        /// The comment count.
        /// </summary>
        public int CommentsCount { get; private set; }

        /// <summary>
        /// The current read page.
        /// </summary>
        public int? Page { get; private set; }

        /// <summary>
        /// The current read chapter.
        /// </summary>
        public int? Chapter { get; private set; }

        /// <summary>
        /// The current read percent.
        /// </summary>
        public int? Percent { get; private set; }

        /// <summary>
        /// The last date of following comment.
        /// </summary>
        public DateTime? LastCommentAt { get; private set; }

        /// <summary>
        /// The ratings count.
        /// </summary>
        public int RatingsCount { get; private set; }

        /// <summary>
        /// Work id.
        /// </summary>
        public int? WorkId { get; private set; }

        /// <summary>
        /// The following note url
        /// </summary>
        public string NoteUrl { get; private set; }

        public DateTime? NoteUpdatedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0}. Body: {1}", Id, Body);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Body = element.ElementAsString("body");
            CreatedAt = element.ElementAsDateTime("created_at");
            UpdatedAt = element.ElementAsDateTime("updated_at");
            CommentsCount = element.ElementAsInt("comments_count");
            Page = element.ElementAsNullableInt("page");
            Chapter = element.ElementAsNullableInt("chapter");
            Percent = element.ElementAsNullableInt("percent");
            LastCommentAt = element.ElementAsDateTime("last_comment_at");
            RatingsCount = element.ElementAsInt("ratings_count");
            WorkId = element.ElementAsNullableInt("work_id");
            NoteUrl = element.ElementAsString("note_uri");
            LastCommentAt = element.ElementAsDateTime("note_updated_at");
        }
    }
}
