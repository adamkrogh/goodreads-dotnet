using System;

namespace Goodreads.Tests
{
    public static class Helper
    {
        public static IGoodreadsClient GetClient()
        {
            return GoodreadsClient.Create(
                Environment.GetEnvironmentVariable("GOODREADS_APIKEY"),
                Environment.GetEnvironmentVariable("GOODREADS_APISECRET"));
        }

        public static IOAuthGoodreadsClient GetAuthClient()
        {
            return GoodreadsClient.CreateAuth(
                        Environment.GetEnvironmentVariable("GOODREADS_APIKEY"),
                        Environment.GetEnvironmentVariable("GOODREADS_APISECRET"),
                        Environment.GetEnvironmentVariable("GOODREADS_OAUTHTOKEN"),
                        Environment.GetEnvironmentVariable("GOODREADS_OAUTHTOKENSECRET"));
        }

        public static long GetUserId()
        {
            var id = Environment.GetEnvironmentVariable("GOODREADS_USERID");
            return string.IsNullOrWhiteSpace(id) ? 0 : long.Parse(id);
        }
    }
}
