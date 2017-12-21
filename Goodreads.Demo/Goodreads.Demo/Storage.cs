using System.Collections.Generic;

namespace Goodreads.Demo
{
    public static class Storage
    {
        // Define your Goodreads key and secret.
        // This can be obtained from https://www.goodreads.com/api/keys.
        public const string ApiKey = "dL9yWNsppzRPPCqX6ZK6Wg";
        public const string ApiSecret = "AD4sWTF3fJNoMfWSwoOFBS3bnu9ksxXGT0runU2k";

        private static Dictionary<string, string> _tokens = new Dictionary<string, string>();

        public static void SaveToken(string token, string secret)
        {
            if (!_tokens.ContainsKey(token))
                _tokens.Add(token, secret);
        }

        public static string GetSecretToken(string token)
        {
            return _tokens.ContainsKey(token) ? _tokens[token] : null;
        }
    }
}
