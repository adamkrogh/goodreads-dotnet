using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Goodreads.Exceptions;
using Goodreads.Models;
using RestSharp;
using RestSharp.Extensions;

namespace Goodreads.Extensions
{
    internal static class RestSharpExtensions
    {
        public static async Task<T> ExecuteTask<T>(this IRestClient client, IRestRequest request)
            where T : ApiResponse, new()
        {
            var ret = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
            return ret.ThrowIfException().Deserialize<T>();
        }

        public static async Task<IRestResponse> ExecuteTaskRaw(this IRestClient client, IRestRequest request)
        {
            var ret = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
            request.OnBeforeDeserialization(ret);
            return ret.ThrowIfException();
        }

        public static T Deserialize<T>(this IRestResponse response)
            where T : ApiResponse, new()
        {
            response.Request.OnBeforeDeserialization(response);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            XDocument document = null;

            try
            {
                document = XDocument.Parse(response.Content);
            }
            catch (XmlException)
            {
                return null;
            }

            if (document == null ||
                document.Root == null ||
                document.Root.Name == "error")
            {
                return null;
            }
            else
            {
                var root = document.Element("GoodreadsResponse");
                var contentRoot = root.XPathSelectElement(response.Request.RootElement);
                var responseObject = new T();
                responseObject.Parse(contentRoot);
                return responseObject;
            }
        }

        private static IRestResponse ThrowIfException(this IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                throw new ApiException(
                    "There was an an exception thrown during the request.",
                    response.ErrorException);
            }

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw response.ResponseStatus.ToWebException();
            }

            if ((int)response.StatusCode == 500)
            {
                throw new ApiException(response.StatusCode, "An unknown error occurred with the Goodreads API.");
            }

            return response;
        }
    }
}
