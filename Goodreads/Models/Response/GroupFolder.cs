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
    public class GroupFolder : ApiResponse
    {
        /// <summary>
        /// The Goodreads Group folder Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The group folder name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The count of group items.
        /// </summary>
        public int ItemsCount { get; protected set; }

        /// <summary>
        /// The group folder link.
        /// </summary>
        public string Link { get; protected set; }

        /// <summary>
        /// The count of group sub items.
        /// </summary>
        public int SubItemsCount { get; protected set; }

        /// <summary>
        /// The group folder updated date.
        /// </summary>
        public DateTime? UpdatedDate { get; protected set; }

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
            Id = element.ElementAsInt("id");
            Name = element.ElementAsString("name");
            ItemsCount = element.ElementAsInt("items_count");
            Link = element.ElementAsString("link");
            SubItemsCount = element.ElementAsInt("sub_items_count");
            UpdatedDate = element.ElementAsDateTime("updated_at");
        }
    }
}
