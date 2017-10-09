using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// This class models am action as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class Action : ApiResponse
    {
        /// <summary>
        /// The action rating.
        /// </summary>
        public int Rating { get; private set; }

        /// <summary>
        /// The action type.
        /// </summary>
        public string ActionType { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Type: {0}, Rating: {1}",
                    ActionType,
                    Rating);
            }
        }

        internal override void Parse(XElement element)
        {
            Rating = element.ElementAsInt("rating");
            ActionType = element.AttributeAsString("type");
        }
    }
}
