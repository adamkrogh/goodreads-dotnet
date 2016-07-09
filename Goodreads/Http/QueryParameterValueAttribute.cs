using System;

namespace Goodreads.Http
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal sealed class QueryParameterValueAttribute : Attribute
    {
        public QueryParameterValueAttribute(string queryParameterValue)
        {
            QueryParameterValue = queryParameterValue;
        }

        public string QueryParameterValue { get; private set; }
    }
}
