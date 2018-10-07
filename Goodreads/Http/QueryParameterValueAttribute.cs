using System;

namespace Goodreads.Http
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class QueryParameterValueAttribute : Attribute
    {
        public QueryParameterValueAttribute(string queryParameterValue)
        {
            QueryParameterValue = queryParameterValue;
        }

        public string QueryParameterValue { get; }
    }
}
