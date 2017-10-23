using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// very brief information about an Author instead of their entire profile.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class GroupSummary : ApiResponse
    {
        /// <summary>
        /// The Goodreads Group Id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The Goodreads group title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The Goodreads group access.
        /// </summary>
        public string Access { get; private set; }

        /// <summary>
        /// The Goodreads group users count.
        /// </summary>
        public int UsersCount { get; private set; }

        /// <summary>
        /// The group image, regular size.
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The group image, small size.
        /// </summary>
        public string SmallImageUrl { get; private set; }

        /// <summary>
        /// The group link.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The last activity of group.
        /// </summary>
        public DateTime? LastActivity { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "GroupSummary: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Title = element.ElementAsString("title");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            Access = element.ElementAsString("access");
            Link = element.ElementAsString("link");
            LastActivity = element.ElementAsDateTime("last_activity_at");
            UsersCount = element.ElementAsInt("users_count");
        }
    }
}
