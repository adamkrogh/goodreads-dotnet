using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents a user's shelf on their Goodreads profile.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UserShelf : ApiResponse
    {
        /// <summary>
        /// The Id of this user shelf.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The name of this user shelf.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The number of books on this user shelf.
        /// </summary>
        public int BookCount { get; protected set; }

        /// <summary>
        /// Determines if this shelf is exclusive or not.
        /// A single book can only be on one exclusive shelf.
        /// </summary>
        public bool IsExclusive { get; protected set; }

        /// <summary>
        /// The description of this user shelf.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Determines the default sort column of this user shelf.
        /// </summary>
        public string Sort { get; protected set; }

        /// <summary>
        /// Determines the default sort order of this user shelf.
        /// </summary>
        public Order? Order { get; protected set; }

        /// <summary>
        /// Determines if this shelf will be featured on the user's profile.
        /// </summary>
        public bool IsFeatured { get; protected set; }

        /// <summary>
        /// Determines if this user shelf is used in recommendations.
        /// </summary>
        public bool IsRecommendedFor { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "UserShelf: Id: {0}, Name: {1}",
                    Id,
                    Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Name = element.ElementAsString("name");
            BookCount = element.ElementAsInt("book_count");
            Description = element.ElementAsString("description");
            Sort = element.ElementAsString("sort");
            IsExclusive = element.ElementAsBool("exclusive_flag");
            IsFeatured = element.ElementAsBool("featured");
            IsRecommendedFor = element.ElementAsBool("recommended_for");

            var orderElement = element.Element("order");
            if (orderElement != null)
            {
                var orderValue = orderElement.Value;
                if (!string.IsNullOrWhiteSpace(orderValue))
                {
                    if (orderValue == "a")
                    {
                        Order = Response.Order.Ascending;
                    }
                    else if (orderValue == "d")
                    {
                        Order = Response.Order.Descending;
                    }
                }
            }
        }
    }
}
