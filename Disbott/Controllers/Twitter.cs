using System;
using Tweetinvi;

namespace Disbott.Controllers
{
    public static class TwitterController
    {
        public static void Authenticate()
        {
            Auth.SetUserCredentials(
                Environment.GetEnvironmentVariable("twitter_consumer_key"),
                Environment.GetEnvironmentVariable("twitter_consumer_secret"),
                Environment.GetEnvironmentVariable("twitter_access_token"),
                Environment.GetEnvironmentVariable("twitter_access_token_secret")
            );
        }
    }
}
