using System;
using System.Globalization;
using System.Xml.Linq;

namespace Goodreads.Extensions
{
    internal static class XmlExtensions
    {
        public static string ElementAsString(this XElement element, XName name)
        {
            return element.Element(name).Value;
        }

        public static int ElementAsInt(this XElement element, XName name)
        {
            var value = element.Element(name).Value;
            var intValue = 0;
            int.TryParse(value, out intValue);
            return intValue;
        }

        public static DateTime? ElementAsDate(this XElement element, XName name)
        {
            var value = element.Element(name).Value;
            DateTime date;
            if (DateTime.TryParseExact(value, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }

        public static bool ElementAsBool(this XElement element, XName name)
        {
            var value = element.Element(name).Value;
            bool boolValue = false;
            bool.TryParse(value, out boolValue);
            return boolValue;
        }
    }
}
