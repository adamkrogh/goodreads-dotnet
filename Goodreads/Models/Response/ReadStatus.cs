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
        public int Id { get; set; }

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

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "ReadStatus: Id: {0}",
                    Id);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Status = element.ElementAsString("status");
            UpdatedAt = element.ElementAsDateTime("updated_at");
            RatingsCount = element.ElementAsInt("ratings_count");
            CommentsCount = element.ElementAsInt("comments_count");
        }
    }
}
