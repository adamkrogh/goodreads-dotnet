using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models an Group folder as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class GroupFolder : ApiResponse
    {
        /// <summary>
        /// The Goodreads Group folder Id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The group folder name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The count of group items.
        /// </summary>
        public int ItemsCount { get; private set; }

        /// <summary>
        /// The group folder link.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The count of group sub items.
        /// </summary>
        public int SubItemsCount { get; private set; }

        /// <summary>
        /// The group folder updated date.
        /// </summary>
        public DateTime? UpdatedDate { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Folder: Id: {0}, Name: {1}",
                    Id,
                    Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Name = element.ElementAsString("name");
            ItemsCount = element.ElementAsInt("items_count");
            Link = element.ElementAsString("link");
            SubItemsCount = element.ElementAsInt("sub_items_count");
            UpdatedDate = element.ElementAsDateTime("updated_at");
        }
    }
}
