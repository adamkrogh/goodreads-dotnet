using System.Diagnostics;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models areas of the API where Goodreads returns
    /// information about actor who made notification.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Actor : ApiResponse
    {
        /// <summary>
        /// An actor Goodreads id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// An actor name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///  An actor display name.
        /// </summary>
        public string DisplayName { get; protected set; }

        /// <summary>
        /// An actor location.
        /// </summary>
        public string Location { get; protected set; }

        /// <summary>
        /// An actor link to Goodreads profile.
        /// </summary>
        public string Link { get; protected set; }

        /// <summary>
        /// The profile image for the actor, regular size.
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// The profile image for the actor, small size.
        /// </summary>
        public string SmallImageUrl { get; protected set; }

        /// <summary>
        /// Determine whether actor has profile image.
        /// </summary>
        public bool HasImage { get; protected set; }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
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
