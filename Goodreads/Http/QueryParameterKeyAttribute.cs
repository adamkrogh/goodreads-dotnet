using System;

namespace Goodreads.Http
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    internal class QueryParameterKeyAttribute : Attribute
    {
        public QueryParameterKeyAttribute(string queryParameterKey)
        {
            QueryParameterKey = queryParameterKey;
        }

        public string QueryParameterKey { get; private set; }
    }
}
