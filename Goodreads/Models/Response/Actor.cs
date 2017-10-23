using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about actor who made notification.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class Actor : ApiResponse
    {
        /// <summary>
        /// An actor Goodreads id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// An actor name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///  An actor display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// An actor location.
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// An actor link to Goodreads profile.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The profile image for the actor, regular size.
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The profile image for the actor, small size.
        /// </summary>
        public string SmallImageUrl { get; private set; }

        /// <summary>
        /// Determine whether actor has profile image.
        /// </summary>
        public bool HasImage { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: {0}, Name: {1}",
                    Id,
                    Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Name = element.ElementAsString("name");
            DisplayName = element.ElementAsString("display_name");
            Location = element.ElementAsString("location");
            Link = element.ElementAsString("link");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            HasImage = element.ElementAsBool("has_image");
        }
    }
}
