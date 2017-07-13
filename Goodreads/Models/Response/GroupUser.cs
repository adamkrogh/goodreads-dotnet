using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models an Group user as defined by the Goodreads API.
    /// </summary>
    public class GroupUser : ApiResponse
    {
        /// <summary>
        /// The Goodreads Group user Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The group user name.
        /// </summary>
        public string UserName { get; protected set; }

        /// <summary>
        /// The group user first name.
        /// </summary>
        public string FirstName { get; protected set; }

        /// <summary>
        /// The group user last name.
        /// </summary>
        public string LastName { get; protected set; }

        /// <summary>
        /// The group user image.
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// The group user title.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The count of comments.
        /// </summary>
        public int CommentsCount { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Group user: Id: {0}, Title: {1}, Name: {2} {3}",
                    Id,
                    Title,
                    FirstName,
                    LastName);
            }
        }

        internal override void Parse(XElement element)
        {
            CommentsCount = element.ElementAsInt("comments_count");
            Title = element.ElementAsString("title");

            var user = element.Element("user");

            Id = user.ElementAsInt("id");
            UserName = user.ElementAsString("user_name");
            FirstName = user.ElementAsString("first_name");
            LastName = user.ElementAsString("last_name");
            ImageUrl = user.ElementAsString("p2_image_url");
        }
    }
}
