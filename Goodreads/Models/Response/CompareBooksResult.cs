using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents a compare books as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class CompareBooksResult : ApiResponse
    {
        /// <summary>
        /// Count of books are not a common.
        /// </summary>
        public int NotInCommon { get; private set; }

        /// <summary>
        /// Your libary compare percent.
        /// </summary>
        public decimal YourLibraryPercent { get; private set; }

        /// <summary>
        /// Their libary compare percent.
        /// </summary>
        public decimal TheirLibraryPercent { get; private set; }

        /// <summary>
        /// Your total books count.
        /// </summary>
        public int YourTotalBooksCount { get; private set; }

        /// <summary>
        /// Their total books count.
        /// </summary>
        public int TheirTotalBooksCount { get; private set; }

        /// <summary>
        /// Count of common books.
        /// </summary>
        public int CommonCount { get; private set; }

        /// <summary>
        /// Compare reviews.
        /// </summary>
        public IReadOnlyList<CompareBookReview> Reviews { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Common: {0}. Not In common: {1}",
                    CommonCount,
                    NotInCommon);
            }
        }

        internal override void Parse(XElement element)
        {
            NotInCommon = element.ElementAsInt("not_in_common");
            YourLibraryPercent = element.ElementAsDecimal("your_library_percent");
            TheirLibraryPercent = element.ElementAsDecimal("their_library_percent");
            YourTotalBooksCount = element.ElementAsInt("your_total_books_count");
            TheirTotalBooksCount = element.ElementAsInt("their_total_books_count");
            CommonCount = element.ElementAsInt("common_count");

            var reviews = element.Element("reviews");
            if (reviews != null)
            {
                var list = new PaginatedList<CompareBookReview>();
                list.Parse(reviews);
                Reviews = list.List;
            }
        }
    }
}
