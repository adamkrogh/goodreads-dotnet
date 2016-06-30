using System.Reflection;
using Goodreads.Http;

namespace Goodreads.Helpers
{
    internal static class EnumHelpers
    {
        public static string QueryParameterKey<T>() where T : struct
        {
            var attribute = typeof(T).GetCustomAttribute<QueryParameterKeyAttribute>();
            return attribute != null
                ? attribute.QueryParameterKey
                : string.Empty;
        }

        public static string QueryParameterValue<T>(T source) where T : struct
        {
            var fieldInfo = source.GetType().GetField(source.ToString());
            var attribute = (QueryParameterValueAttribute)fieldInfo.GetCustomAttribute(typeof(QueryParameterValueAttribute), false);

            return attribute != null
                ? attribute.QueryParameterValue
                : null;
        }
    }
}
