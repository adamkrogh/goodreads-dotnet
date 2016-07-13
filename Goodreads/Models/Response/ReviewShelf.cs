using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents a shelf that a review is in.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReviewShelf : ApiResponse
    {
        /// <summary>
        /// The Id of this review shelf, optional.
        /// </summary>
        public int? Id { get; protected set; }

        /// <summary>
        /// The name of the shelf.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Determines if a book can exclusively appear on this shelf only.
        /// </summary>
        public bool IsExclusive { get; protected set; }

        /// <summary>
        /// Determines if this shelf is sortable.
        /// </summary>
        public bool IsSortable { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "ReviewShelf: Name: {0}",
                    Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.AttributeAsNullableInt("review_shelf_id");
            Name = element.AttributeAsString("name");
            IsExclusive = element.AttributeAsBool("exclusive");
            IsSortable = element.AttributeAsBool("sortable");
        }
    }
}
