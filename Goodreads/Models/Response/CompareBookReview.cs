using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

namespace Goodreads.Models.Response
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class CompareBookReview : ApiResponse
    {
        /// <summary>
        /// A compare book.
        /// </summary>
        public CompareBook Book { get; private set; }

        /// <summary>
        /// Your review.
        /// </summary>
        public ShortReview YourReview { get; private set; }

        /// <summary>
        /// Their review.
        /// </summary>
        public ShortReview TheirReview { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Book: {0}. [{1} - {2}]",
                    Book.Title,
                    YourReview.Rating,
                    TheirReview.Rating);
            }
        }

        internal override void Parse(XElement element)
        {
            var book = element.Element("book");
            if (book != null)
            {
                Book = new CompareBook();
                Book.Parse(book);
            }

            var yourReview = element.Element("your_review");
            if (yourReview != null)
            {
                YourReview = new ShortReview();
                YourReview.Parse(yourReview);
            }

            var theirReview = element.Element("their_review");
            if (theirReview != null)
            {
                TheirReview = new ShortReview();
                TheirReview.Parse(theirReview);
            }
        }
    }
}
