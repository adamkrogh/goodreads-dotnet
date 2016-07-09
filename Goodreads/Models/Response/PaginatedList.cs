using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents a paginated list of objects as returned by the Goodreads API,
    /// along with pagination information about the page size, current page, etc...
    /// </summary>
    /// <typeparam name="T">The type of the object in the paginated list.</typeparam>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PaginatedList<T> : ApiResponse where T : ApiResponse, new()
    {
        /// <summary>
        /// The list of objects for the current page.
        /// </summary>
        public IReadOnlyList<T> List { get; protected set; }

        /// <summary>
        /// Pagination information about the list and current page.
        /// </summary>
        public PaginationModel Pagination { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "PaginatedList: CurrentItems: {0}, TotalItems: {1}",
                    List == null ? 0 : List.Count,
                    Pagination == null ? 0 : Pagination.TotalItems);
            }
        }

        /// <summary>
        /// Determine the page size of the paginated list.
        /// </summary>
        /// <remarks>
        /// I wanted to add a PageSize to the paginated list so we can provide some extra properties.
        /// This resulted in some issues as Goodreads isn't consistent at all with page sizes.
        /// This method includes some heuristics for guessing the page size based on the XElement.
        /// I might have to remove PageSize in the future if this is unmaintainable...
        /// </remarks>
        /// <param name="element">The element to determine the page size for.</param>
        /// <returns>The page size for the given element.</returns>
        internal static int DeterminePageSize(XElement element)
        {
            if (element == null)
            {
                return 0;
            }

            if (element.Name == "books")
            {
                return 30;
            }

            if (element.Name == "shelves")
            {
                return 100;
            }

            if (element.Name == "friends")
            {
                return 30;
            }

            if (element.Name == "search")
            {
                return 20;
            }

            // TODO: this is wrong, reviews page size is dynamic...
            if (element.Name == "reviews")
            {
                return 20;
            }

            return 0;
        }

        internal override void Parse(XElement element)
        {
            Pagination = new PaginationModel(DeterminePageSize(element));
            Pagination.Parse(element);

            // Should have known search pagination would be different...
            if (element.Name == "search")
            {
                var results = element.Descendants("results");
                if (results != null && results.Count() == 1)
                {
                    List = results.First().ParseChildren<T>();
                }
            }
            else
            {
                List = element.ParseChildren<T>();
            }
        }
    }
}
