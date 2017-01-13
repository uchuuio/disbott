using System;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Models;

namespace Disbott.Controllers
{
    public static class TwitterController
    {
        /// <summary>
        /// Twitter auth method, variables hidden
        /// </summary>
        public static void Authenticate()
        {
            Auth.SetUserCredentials(
                Environment.GetEnvironmentVariable("twitter_consumer_key"),
                Environment.GetEnvironmentVariable("twitter_consumer_secret"),
                Environment.GetEnvironmentVariable("twitter_access_token"),
                Environment.GetEnvironmentVariable("twitter_access_token_secret")
            );
        }
        /// <summary>
        /// Gets the latest tweet by a user from the twitter api
        /// </summary>
        /// <param name="twitterAccount"></param>
        /// <returns></returns>
        public static string GetUsersLatestTweet(string twitterAccount)
        {
            Authenticate();
            var userTweets = Timeline.GetUserTimeline(twitterAccount, 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;
            return tweetMsg;
        }

        /// <summary>
        /// Gets a random tweet from a user from the twitter api
        /// </summary>
        /// <param name="twitterAccount"></param>
        /// <returns></returns>
        public static string GetUsersRandomTweet(string twitterAccount)
        {
            Random rnd = new Random();
            Authenticate();
            var userTweets = Timeline.GetUserTimeline(twitterAccount);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var selectedTweetNumber = rnd.Next(0, 39);
            var tweetMsg = tweets[selectedTweetNumber].FullText;
            return tweetMsg;
        }
    }
}
