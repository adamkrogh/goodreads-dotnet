using System;
using System.Net;

namespace Goodreads.Exceptions
{
    /// <summary>
    /// Represents errors that occur from the Goodreads API.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code of the API response.</param>
        /// <param name="message">A message describing the exception.</param>
        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// The status code of the Goodreads response.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
    }
}
