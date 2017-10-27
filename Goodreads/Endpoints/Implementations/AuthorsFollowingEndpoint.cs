using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Goodreads.Http;
using Goodreads.Models.Response;
using RestSharp;

namespace Goodreads.Clients
{
    /// <summary>
    /// The client class for the Author_following endpoint of the Goodreads API.
    /// </summary>
    internal sealed class AuthorsFollowingEndpoint : Endpoint, IOAuthAuthorsFollowingEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorsFollowingEndpoint"/> class.
        /// </summary>
        /// <param name="connection">A RestClient connection to the Goodreads API.</param>
        public AuthorsFollowingEndpoint(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Follow an author.
        /// </summary>
        /// <param name="authorId">The Goodreads Id for the desired author.</param>
        /// <returns>A Goodreads author following model.</returns>
        public async Task<AuthorFollowing> Follow(long authorId)
        {
            var parameters = new List<Parameter>
            {
                new Parameter { Name = "id", Value = authorId, Type = ParameterType.QueryString }
            };

            return await Connection.ExecuteRequest<AuthorFollowing>(
                    "author_followings",
                    parameters,
                    null,
                    "author_following",
                    Method.POST)
                    .ConfigureAwait(false);
        }

        /// <summary>
        /// Unfollow an author.
        /// </summary>
        /// <param name="authorFollowingId">The Goodreads Id for the desired author.</param>
        /// <returns>True if the unfollow succeeded, false otherwise.</returns>
        public async Task<bool> Unfollow(long authorFollowingId)
        {
            var endpoint = string.Format(CultureInfo.InvariantCulture, @"author_followings/{0}", authorFollowingId);
            var response = await Connection.ExecuteRaw(endpoint, null, Method.DELETE).ConfigureAwait(false);

            return response.StatusCode == HttpStatusCode.NoContent;
        }

        /// <summary>
        /// Show author following information.
        /// </summary>
        /// <param name="authorFollowingId">The Goodreads Id for the desired author.</param>
        /// <returns>A Goodreads author following model.</returns>
        public async Task<AuthorFollowing> Show(long authorFollowingId)
        {
            var endpoint = string.Format(CultureInfo.InvariantCulture, "author_followings/{0}", authorFollowingId);

            return await Connection.ExecuteRequest<AuthorFollowing>(endpoint, null, null, "author_following", Method.GET)
                .ConfigureAwait(false);
        }
    }
}
