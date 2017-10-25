using System;

namespace Goodreads.Tests
{
    public static class Helper
    {
        public static IGoodreadsClient GetClient()
        {
            return GoodreadsClient.Create("dL9yWNsppzRPPCqX6ZK6Wg", "AD4sWTF3fJNoMfWSwoOFBS3bnu9ksxXGT0runU2k");
        }

        public static IOAuthGoodreadsClient GetAuthClient()
        {
            return GoodreadsClient.Create("dL9yWNsppzRPPCqX6ZK6Wg", "AD4sWTF3fJNoMfWSwoOFBS3bnu9ksxXGT0runU2k", "5NgfM2zlZmp0hcU8xOW5rg", "US29b1rPusqHQ3Ui1MCFEvskEGy3Fzykz260oTJ6SO0");
        }

        public static long GetUserId()
        {
            var id = "68628513"; // Environment.GetEnvironmentVariable("GOODREADS_USERID");
            return string.IsNullOrWhiteSpace(id) ? 0 : long.Parse(id);
        }
    }
}
