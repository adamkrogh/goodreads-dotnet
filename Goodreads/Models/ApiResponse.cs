using System.Xml.Linq;

namespace Goodreads.Models
{
    /// <summary>
    /// A class for all responses from the Goodreads API.
    /// </summary>
    public abstract class ApiResponse
    {
        internal abstract void Parse(XElement element);
    }
}
