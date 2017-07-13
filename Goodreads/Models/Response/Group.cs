using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models an Group as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Group : ApiResponse
    {
        /// <summary>
        /// The Goodreads Group Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The group title.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The group description.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The Goodreads group access.
        /// </summary>
        public string Access { get; protected set; }

        /// <summary>
        /// The group location.
        /// </summary>
        public string Location { get; protected set; }

        /// <summary>
        /// The group last activity.
        /// </summary>
        public DateTime? LastActivity { get; protected set; }

        /// <summary>
        /// Determine whether the current user is member of this group.
        /// </summary>
        public bool IsMember { get; protected set; }

        /// <summary>
        /// The count of display folder for group.
        /// </summary>
        public int DisplayFolderCount { get; protected set; }

        /// <summary>
        /// The count of topics per group folder.
        /// </summary>
        public int DisplayTopicsPerFolder { get; protected set; }

        /// <summary>
        /// Determine whether book shelves are public.
        /// </summary>
        public bool AreBookshelvesPublic { get; protected set; }

        /// <summary>
        /// Determine ability for adding books to group.
        /// </summary>
        public bool CanAddBooks { get; protected set; }

        /// <summary>
        /// Determine ability for adding events to group.
        /// </summary>
        public bool CanAddEvents { get; protected set; }

        /// <summary>
        /// Determine ability for adding polls to group.
        /// </summary>
        public bool CanAddPolls { get; protected set; }

        /// <summary>
        /// Determine publicity of discussions.
        /// </summary>
        public bool AreDiscussionsPublic { get; protected set; }

        /// <summary>
        /// The group real world property.
        /// </summary>
        public bool IsRealWorld { get; protected set; }

        /// <summary>
        /// Determine ability for accepting new group members.
        /// </summary>
        public bool CanAcceptNewMembers { get; protected set; }

        /// <summary>
        /// The group category.
        /// </summary>
        public string Category { get; protected set; }

        /// <summary>
        /// The group subcategory.
        /// </summary>
        public string SubCategory { get; protected set; }

        /// <summary>
        /// The group rules.
        /// </summary>
        public string Rules { get; private set; }

        /// <summary>
        /// The group image, regular size.
        /// </summary>
        public string ImageUrl { get; protected set; }

        /// <summary>
        /// The group link.
        /// </summary>
        public string Link { get; protected set; }

        /// <summary>
        /// The Goodreads group users count.
        /// </summary>
        public int UsersCount { get; protected set; }

        /// <summary>
        /// The collection of group folders.
        /// </summary>
        public IReadOnlyList<GroupFolder> Folders { get; protected set; }

        /// <summary>
        /// The collection of group moderators.
        /// </summary>
        public IReadOnlyList<GroupUser> Moderators { get; protected set; }

        /// <summary>
        /// The collection of group members.
        /// </summary>
        public IReadOnlyList<GroupUser> Members { get; protected set; }

        /// <summary>
        /// The collection of currently reading books.
        /// </summary>
        public IReadOnlyList<GroupBook> CurrentlyReading { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Group: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsInt("id");
            Title = element.ElementAsString("title");
            Description = element.ElementAsString("description");
            Access = element.ElementAsString("access");
            Location = element.ElementAsString("location");
            LastActivity = element.ElementAsDateTime("last_activity_at");
            IsMember = element.ElementAsBool("is_member");
            DisplayFolderCount = element.ElementAsInt("display_folder_count");
            DisplayTopicsPerFolder = element.ElementAsInt("display_topics_per_folder");
            AreBookshelvesPublic = element.ElementAsBool("bookshelves_public_flag");
            CanAddBooks = element.ElementAsBool("add_books_flag");
            CanAddEvents = element.ElementAsBool("add_events_flag");
            CanAddPolls = element.ElementAsBool("polls_flag");
            AreDiscussionsPublic = element.ElementAsBool("discussion_public_flag");
            IsRealWorld = element.ElementAsBool("real_world_flag");
            CanAcceptNewMembers = element.ElementAsBool("accepting_new_members_flag");
            Category = element.ElementAsString("category");
            SubCategory = element.ElementAsString("subcategory");
            Rules = element.ElementAsString("rules");
            ImageUrl = element.ElementAsString("image_url");
            Link = element.ElementAsString("link");
            UsersCount = element.ElementAsInt("users_count");

            var folders = element.ParseChildren<GroupFolder>("folders", "folder");
            if (folders != null)
            {
                Folders = folders;
            }

            var moderators = element.ParseChildren<GroupUser>("moderators", "group_user");
            if (moderators != null)
            {
                Moderators = moderators;
            }

            var members = element.ParseChildren<GroupUser>("members", "group_user");
            if (members != null)
            {
                Members = members;
            }

            var books = element.ParseChildren<GroupBook>("currently_reading", "group_book");
            if (books != null)
            {
                CurrentlyReading = books;
            }
        }
    }
}
