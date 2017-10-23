using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents information about an user status.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class UserStatusSummary : ApiResponse
    {
        /// <summary>
        /// The user status id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The user status action text.
        /// </summary>
        public string ActionText { get; private set; }

        /// <summary>
        /// The user status link.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The user status image url, regular size.
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// A user who create status.
        /// </summary>
        public Actor User { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "UserStatusSummary: Id: {0}. Text: {1} {2}",
                    Id,
                    User?.Name,
                    ActionText);
            }
        }

        internal override void Parse(XElement element)
        {
            ActionText = element.ElementAsString("action_text");
            Link = element.ElementAsString("link");
            ImageUrl = element.ElementAsString("image_url");
            Id = element.ElementAsNullableLong("id") ?? ParseId();

            var actor = element.Element("actor");
            if (actor != null)
            {
                User = new Actor();
                User.Parse(actor);
            }
        }

        private long ParseId()
        {
            var index = Link.LastIndexOf('/');
            if (index > 0 && index != Link.Length - 1)
            {
                if (long.TryParse(Link.Substring(index + 1), out long id))
                {
                    return id;
                }
            }

            return 0;
        }
    }
}
