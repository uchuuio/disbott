using System;
using System.Linq;
using System.Threading.Tasks;

using Tweetinvi;

using Discord.Commands;
using Tweetinvi.Models;

namespace Disbott.Views
{
    [Name("Twitter")]
    public class TwitterModule : ModuleBase
    {
        public void Authenticate()
        {
            Auth.SetUserCredentials(
                ConfigurationManager.AppSettings["twitter_consumer_key"],
                ConfigurationManager.AppSettings["twitter_consumer_secret"],
                ConfigurationManager.AppSettings["twitter_access_token"],
                ConfigurationManager.AppSettings["twitter_access_token_secret"]
            );
        }

        [Command("headline")]
        [Remarks("Gets the latest tweet from The Guardian's Twitter Account")]
        public async Task Headline()
        {
            Authenticate();
            var userTweets = Timeline.GetUserTimeline("guardian", 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;

            await ReplyAsync(tweetMsg);
        }

        [Command("mirin")]
        [Remarks("Gets the latest tweet from Mirin Furukawa's Twitter Account")]
        public async Task Mirin()
        {
            Authenticate();
            var userTweets = Timeline.GetUserTimeline("furukawamirin", 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;

            await ReplyAsync(tweetMsg);
        }

        [Command("gumi")]
        [Remarks("Gets the latest tweet from Nanase Gumi's Twitter Account")]
        public async Task Gumi()
        {
            Authenticate();
            var userTweets = Timeline.GetUserTimeline("gumi_nanase", 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;

            await ReplyAsync(tweetMsg);
        }

        [Command("gazo")]
        [Remarks("NSFW, gets a random image from @idol_gazo twitter account")]
        public async Task Gazo()
        {
            Random rnd = new Random();
            Authenticate();

            var userTweets = Timeline.GetUserTimeline("idol_gazo");
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var selectedTweetNumber = rnd.Next(0, 39);
            var tweetMsg = tweets[selectedTweetNumber].FullText;

            await ReplyAsync(tweetMsg);
        }
    }
}
