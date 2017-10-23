using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents a user as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class User : ApiResponse
    {
        /// <summary>
        /// The Id of the user.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// The link to the user's Goodreads profile page.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The profile image for the user, regular size.
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// THe profile image for the user, small size.
        /// </summary>
        public string SmallImageUrl { get; private set; }

        /// <summary>
        /// The about description of the user.
        /// </summary>
        public string About { get; private set; }

        /// <summary>
        /// The age of the user, if given.
        /// </summary>
        public int? Age { get; private set; }

        /// <summary>
        /// The gender of the user.
        /// </summary>
        public string Gender { get; private set; }

        /// <summary>
        /// The location of the user.
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// The website of the user.
        /// </summary>
        public string Website { get; private set; }

        /// <summary>
        /// The date the user joined Goodreads.
        /// Note: Goodreads only provides the month and year, this
        /// property defaults to the first day of the given month.
        /// </summary>
        public DateTime? JoinedDate { get; private set; }

        /// <summary>
        /// The date the user was last active on Goodreads.
        /// Note: Goodreads only provides the month and year, this
        /// property defaults to the first day of the given month.
        /// </summary>
        public DateTime? LastActiveDate { get; private set; }

        /// <summary>
        /// The interests of the user.
        /// </summary>
        public string Interests { get; private set; }

        /// <summary>
        /// The favorite books of the user.
        /// </summary>
        public string FavoriteBooks { get; private set; }

        /// <summary>
        /// The list of favorite authors the user has added on Goodreads.
        /// </summary>
        public IReadOnlyList<AuthorSummary> FavoriteAuthors { get; private set; }

        /// <summary>
        /// The RSS url for the user's updates.
        /// </summary>
        public string UpdatesRssUrl { get; private set; }

        /// <summary>
        /// The RSS url for the user's reviews.
        /// </summary>
        public string ReviewsRssUrl { get; private set; }

        /// <summary>
        /// The number of friends the user has on Goodreads.
        /// </summary>
        public int FriendsCount { get; private set; }

        /// <summary>
        /// The number of reviews the user has made on Goodreads.
        /// </summary>
        public int ReviewsCount { get; private set; }

        /// <summary>
        /// The number of groups the user is in.
        /// </summary>
        public int GroupsCount { get; private set; }

        /// <summary>
        /// The shelves the user has organized their books into.
        /// </summary>
        public IReadOnlyList<UserShelf> Shelves { get; private set; }

        // TODO: parse out updates once I determine a structure for them.
        ////public IReadOnlyList<Update> Updates { get; private set; }

        // TODO: parse out user statuses.
        ////public IReadOnlyList<UserStatus> Statuses { get; private set; }

        /// <summary>
        /// Determines if the user's profile is private or not.
        /// </summary>
        public bool IsPrivate { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "User: Id: {0}, Name: {1}",
                    Id,
                    Name);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Name = element.ElementAsString("name");
            Username = element.ElementAsString("user_name");
            Link = element.ElementAsString("link");
            ImageUrl = element.ElementAsString("image_url");
            SmallImageUrl = element.ElementAsString("small_image_url");
            About = element.ElementAsString("about");
            Age = element.ElementAsNullableInt("age");
            Gender = element.ElementAsString("gender");
            Location = element.ElementAsString("location");
            Website = element.ElementAsString("website");
            JoinedDate = element.ElementAsMonthYear("joined");
            LastActiveDate = element.ElementAsMonthYear("last_active");
            Interests = element.ElementAsString("interests");
            FavoriteBooks = element.ElementAsString("favorite_books");
            FavoriteAuthors = element.ParseChildren<AuthorSummary>("favorite_authors", "author");
            UpdatesRssUrl = element.ElementAsString("updates_rss_url");
            ReviewsRssUrl = element.ElementAsString("reviews_rss_url");
            FriendsCount = element.ElementAsInt("friends_count");
            GroupsCount = element.ElementAsInt("groups_count");
            ReviewsCount = element.ElementAsInt("reviews_count");
            Shelves = element.ParseChildren<UserShelf>("user_shelves", "user_shelf");
            IsPrivate = element.ElementAsBool("private");
        }
    }
}
