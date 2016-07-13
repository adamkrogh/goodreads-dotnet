using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models a comment, defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Comment : ApiResponse
    {
        /// <summary>
        /// The id of this comment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The body text of this comment.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The user that made this comment.
        /// </summary>
        public UserSummary User { get; set; }

        /// <summary>
        /// The date and time this comment was created at.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The date and time this comment was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Comment: Id: {0}",
                    Id);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Body = element.ElementAsString("body");

            var userElement = element.Element("user");
            if (userElement != null)
            {
                User = new UserSummary();
                User.Parse(userElement);
            }

            CreatedAt = element.ElementAsDateTime("created_at");
            UpdatedAt = element.ElementAsDateTime("updated_at");
        }
    }
}
