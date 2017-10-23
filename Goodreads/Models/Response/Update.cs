using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models an Update as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class Update : ApiResponse
    {
        /// <summary>
        /// The Goodreads update Id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The update action text.
        /// </summary>
        public string ActionText { get; private set; }

        /// <summary>
        /// The update link.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// The update image. Small size.
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// The author of update.
        /// </summary>
        public Actor User { get; private set; }

        /// <summary>
        /// Updated date.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// The update type.
        /// </summary>
        public string UpdateType { get; private set; }

        /// <summary>
        /// The update read status.
        /// </summary>
        public ReadStatus ReadStatus { get; private set; }

        /// <summary>
        /// The update action.
        /// </summary>
        public Action Action { get; private set; }

        /// <summary>
        /// The update book.
        /// </summary>
        public BookSummary Book { get; private set; }

        /// <summary>
        /// The update body.
        /// </summary>
        public string Body { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Id: Id: {0}, Type: {1}, Text: {2}",
                    Id,
                    UpdateType,
                    ActionText);
            }
        }

        internal override void Parse(XElement element)
        {
            UpdateType = element.AttributeAsString("type");
            Id = element.ElementAsLong("id");
            ActionText = element.ElementAsString("action_text");
            Link = element.ElementAsString("link");
            ImageUrl = element.ElementAsString("image_url");
            UpdatedAt = element.ElementAsDateTime("updated_at");
            Body = element.ElementAsString("body");

            var user = element.Element("actor");
            if (user != null)
            {
                User = new Actor();
                User.Parse(user);
            }

            var action = element.Element("action");
            if (action != null)
            {
                Action = new Action();
                Action.Parse(action);
            }

            var @object = element.Element("object");

            var status = @object?.Element("read_status");
            if (status != null)
            {
                ReadStatus = new ReadStatus();
                ReadStatus.Parse(status);
            }

            var book = @object?.Element("book");
            if (book != null)
            {
                Book = new BookSummary();
                Book.Parse(book);
            }
        }
    }
}
