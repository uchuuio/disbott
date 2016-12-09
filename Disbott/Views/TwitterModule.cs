using System;
using System.Linq;
using System.Threading.Tasks;

using Discord.Commands;
using Tweetinvi;
using Tweetinvi.Models;

using Disbott.Controllers;

namespace Disbott.Views
{
    [Name("Twitter")]
    public class TwitterModule : ModuleBase
    {
        [Command("headline")]
        [Remarks("Gets the latest tweet from The Guardian's Twitter Account")]
        public async Task Headline()
        {
            TwitterController.Authenticate();
            var userTweets = Timeline.GetUserTimeline("guardian", 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;

            await ReplyAsync(tweetMsg);
        }

        [Command("mirin")]
        [Remarks("Gets the latest tweet from Mirin Furukawa's Twitter Account")]
        public async Task Mirin()
        {
            TwitterController.Authenticate();
            var userTweets = Timeline.GetUserTimeline("furukawamirin", 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;

            await ReplyAsync(tweetMsg);
        }

        [Command("gumi")]
        [Remarks("Gets the latest tweet from Nanase Gumi's Twitter Account")]
        public async Task Gumi()
        {
            TwitterController.Authenticate();
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
            TwitterController.Authenticate();

            var userTweets = Timeline.GetUserTimeline("idol_gazo");
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var selectedTweetNumber = rnd.Next(0, 39);
            var tweetMsg = tweets[selectedTweetNumber].FullText;

            await ReplyAsync(tweetMsg);
        }
    }
}
