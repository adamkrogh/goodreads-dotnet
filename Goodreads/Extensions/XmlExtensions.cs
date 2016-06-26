using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Goodreads.Models;

namespace Goodreads.Extensions
{
    internal static class XmlExtensions
    {
        public static string ElementAsString(this XElement element, XName name)
        {
            var stringElement = element.Element(name);
            if (stringElement != null)
            {
                var value = stringElement.Value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return null;
        }

        public static int ElementAsInt(this XElement element, XName name)
        {
            var intElement = element.Element(name);
            if (intElement != null)
            {
                var intValue = 0;
                int.TryParse(intElement.Value, out intValue);
                return intValue;
            }
            else
            {
                return 0;
            }
        }

        public static int? ElementAsNullableInt(this XElement element, XName name)
        {
            var intElement = element.Element(name);
            if (intElement != null)
            {
                var intValue = 0;
                if (int.TryParse(intElement.Value, out intValue))
                {
                    return intValue;
                }
            }

            return null;
        }

        public static decimal ElementAsDecimal(this XElement element, XName name)
        {
            var decimalElement = element.Element(name);
            if (decimalElement != null)
            {
                var decimalValue = 0m;
                decimal.TryParse(decimalElement.Value, out decimalValue);
                return decimalValue;
            }
            else
            {
                return 0m;
            }
        }

        public static decimal? ElementAsNullableDecimal(this XElement element, XName name)
        {
            var decimalElement = element.Element(name);
            if (decimalElement != null)
            {
                var decimalValue = 0m;
                if (decimal.TryParse(decimalElement.Value, out decimalValue))
                {
                    return decimalValue;
                }
            }

            return null;
        }

        public static DateTime? ElementAsDate(this XElement element, XName name)
        {
            var dateElement = element.Element(name);
            if (dateElement != null)
            {
                DateTime date;
                if (DateTime.TryParseExact(dateElement.Value, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
            }

            return null;
        }

        /// <summary>
        /// Goodreads sometimes returns dates as three separate fields.
        /// This method parses out each one and returns a date object.
        /// </summary>
        /// <param name="element">The parent element of the date elements.</param>
        /// <param name="prefix">The common prefix for the three Goodreads date elements.</param>
        /// <returns>A date object after parsing the three Goodreads date fields.</returns>
        public static DateTime? ElementAsGoodreadsDate(this XElement element, string prefix)
        {
            var publicationYear = element.ElementAsNullableInt(prefix + "_year");
            var publicationMonth = element.ElementAsNullableInt(prefix + "_month");
            var publicationDay = element.ElementAsNullableInt(prefix + "_day");

            if (!publicationYear.HasValue &&
                !publicationMonth.HasValue &&
                !publicationDay.HasValue)
            {
                return null;
            }

            if (!publicationYear.HasValue)
            {
                return null;
            }

            if (!publicationDay.HasValue)
            {
                publicationDay = 1;
            }

            if (!publicationMonth.HasValue)
            {
                publicationMonth = 1;
            }

            return new DateTime(publicationYear.Value, publicationMonth.Value, publicationDay.Value);
        }

        public static bool ElementAsBool(this XElement element, XName name)
        {
            var boolElement = element.Element(name);
            if (boolElement != null)
            {
                var boolValue = false;
                bool.TryParse(boolElement.Value, out boolValue);
                return boolValue;
            }
            else
            {
                return false;
            }
        }

        public static List<T> ParseChildren<T>(this XElement element, XName parentName, XName childName) where T : ApiResponse, new()
        {
            return ParseChildren(
                element,
                parentName,
                childName,
                (childElement) =>
            {
                var child = new T();
                child.Parse(childElement);
                return child;
            });
        }

        public static List<T> ParseChildren<T>(this XElement element, XName parentName, XName childName, Func<XElement, T> parseChild)
        {
            var parentElement = element.Element(parentName);
            if (parentElement != null)
            {
                var childElements = parentElement.Descendants(childName);
                if (childElements != null && childElements.Count() > 0)
                {
                    var children = new List<T>();

                    foreach (var childElement in childElements)
                    {
                        children.Add(parseChild(childElement));
                    }

                    return children;
                }
            }

            return null;
        }
    }
}
