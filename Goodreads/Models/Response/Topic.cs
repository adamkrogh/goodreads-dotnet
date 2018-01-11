using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models an Group topic as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class Topic : ApiResponse
    {
        /// <summary>
        /// The topic identifier.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The topic author id.
        /// </summary>
        public long AuthorUserId { get; private set; }

        /// <summary>
        /// The topic author name.
        /// </summary>
        public string AuthorUserName { get; private set; }

        /// <summary>
        /// A topic title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Count of topic comments.
        /// </summary>
        public int CommentsCount { get; private set; }

        /// <summary>
        /// Count of new topic comments.
        /// </summary>
        public int NewCommentsCount { get; private set; }

        /// <summary>
        /// Count comment per one page.
        /// </summary>
        public int CommentsPerPage { get; private set; }

        /// <summary>
        /// Unread comments count per page
        /// </summary>
        public int UnreadCommentsCount { get; private set; }

        /// <summary>
        /// Updated date.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// The last comment date.
        /// </summary>
        public DateTime? LastCommentAt { get; private set; }

        /// <summary>
        /// Topic comments.
        /// </summary>
        public PaginatedList<Comment> Comments { get; private set; }

        /// <summary>
        /// The topic subject type.
        /// </summary>
        public string SubjectType { get; private set; }

        /// <summary>
        /// The topic subject id.
        /// </summary>
        public long? SubjectId { get; private set; }

        /// <summary>
        /// The topic context type.
        /// </summary>
        public string ContextType { get; private set; }

        /// <summary>
        /// The topic context id.
        /// </summary>
        public long? ContextId { get; private set; }

        /// <summary>
        /// Topic folder.
        /// </summary>
        public TopicFolder Folder { get; private set; }

        /// <summary>
        /// Topic group.
        /// </summary>
        public TopicGroup Group { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Topic: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            AuthorUserId = element.ElementAsLong("author_user_id");
            AuthorUserName = element.ElementAsString("author_user_name");
            Title = element.ElementAsString("title");
            CommentsCount = element.ElementAsInt("comments_count");
            NewCommentsCount = element.ElementAsInt("new_comments_count");
            CommentsPerPage = element.ElementAsInt("comments_per_page");
            UnreadCommentsCount = element.ElementAsInt("unread_count");
            UpdatedAt = element.ElementAsDateTime("updated_at");
            LastCommentAt = element.ElementAsDateTime("last_comment_at");
            SubjectType = element.ElementAsString("subject_type");
            SubjectId = element.ElementAsNullableLong("subject_id");
            ContextType = element.ElementAsString("context_type");
            ContextId = element.ElementAsNullableLong("context_id");

            var authorUser = element.Element("author_user");
            if (authorUser != null)
            {
                AuthorUserId = authorUser.ElementAsLong("id");
                AuthorUserName = element.ElementAsString("user_name")
                    ?? (element.ElementAsString("first_name") + " " + element.ElementAsString("last_name"));
            }

            var folder = element.Element("folder");
            if (folder != null)
            {
                Folder = new TopicFolder();
                Folder.Parse(folder);
            }

            var group = element.Element("group");
            if (group != null)
            {
                Group = new TopicGroup();
                Group.Parse(group);
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
