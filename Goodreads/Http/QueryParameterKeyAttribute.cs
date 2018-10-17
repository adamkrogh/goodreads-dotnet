using System;

namespace Goodreads.Http
{
    [AttributeUsage(AttributeTargets.Enum)]
    internal sealed class QueryParameterKeyAttribute : Attribute
    {
        public QueryParameterKeyAttribute(string queryParameterKey)
        {
            QueryParameterKey = queryParameterKey;
        }

        public string QueryParameterKey { get; }
    }
}
