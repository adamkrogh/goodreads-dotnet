using System;
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

        /// <summary>
        /// The author following likes count.
        /// </summary>
        public int LikesCount { get; protected set; }

        /// <summary>
        /// The author following comments count.
        /// </summary>
        public int CommentsCount { get; protected set; }

        /// <summary>
        /// True if following was liked, otherwise false.
        /// </summary>
        public bool Liked { get; protected set; }

        /// <summary>
        /// The author following created date.
        /// </summary>
        public DateTime? CreatedDateTime { get; protected set; }

        /// <summary>
        /// The author who is followed.
        /// </summary>
        public AuthorSummary Author { get; protected set; }

        /// <summary>
        /// The user who follows author.
        /// </summary>
        public UserSummary User { get; private set; }

        /// <summary>
        /// The paginated list of comments that have been made on this review.
        /// </summary>
        public PaginatedList<Comment> Comments { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "AuthorFollowingId: {0}", Id);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            LikesCount = element.ElementAsInt("likes_count");
            Liked = element.ElementAsBool("likes_count");
            CreatedDateTime = element.ElementAsDateTime("created_at");

            var author = element.Element("author");
            if (author != null)
            {
                Author = new AuthorSummary();
                Author.Parse(author);
            }

            var user = element.Element("user");
            if (author != null)
            {
                User = new UserSummary();
                User.Parse(user);
            }

            var commentsElement = element.Element("comments");
            if (commentsElement != null)
            {
                Comments = new PaginatedList<Comment>();
                Comments.Parse(commentsElement);
            }
        }
    }
}
