using System;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Tweetinvi;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Tweetinvi.Models;

namespace Disbott.Modules
{
    [Module]
    public class Twitter
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

        [Command("headline"), Description("Gets the latest tweet from The Guardian's Twitter Account")]
        public async Task Headline(IUserMessage msg)
        {
            this.Authenticate();
            var userTweets = Timeline.GetUserTimeline("guardian", 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;

            await msg.Channel.SendMessageAsync(tweetMsg);
        }

        [Command("mirin"), Description("Gets the latest tweet from Mirin Furukawa's Twitter Account")]
        public async Task Mirin(IUserMessage msg)
        {
            this.Authenticate();
            var userTweets = Timeline.GetUserTimeline("furukawamirin", 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;

            await msg.Channel.SendMessageAsync(tweetMsg);
        }

        [Command("gumi"), Description("Gets the latest tweet from Nanase Gumi's Twitter Account")]
        public async Task Gumi(IUserMessage msg)
        {
            this.Authenticate();
            var userTweets = Timeline.GetUserTimeline("gumi_nanase", 1);
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var tweetMsg = tweets[0].FullText;

            await msg.Channel.SendMessageAsync(tweetMsg);
        }

        [Command("gazo"), Description("NSFW, gets a random image from @idol_gazo twitter account")]
        public async Task Gazo(IUserMessage msg)
        {
            Random rnd = new Random();
            this.Authenticate();

            var userTweets = Timeline.GetUserTimeline("idol_gazo");
            var tweets = userTweets as ITweet[] ?? userTweets.ToArray();
            var selectedTweetNumber = rnd.Next(0, 39);
            var tweetMsg = tweets[selectedTweetNumber].FullText;

            await msg.Channel.SendMessageAsync(tweetMsg);
        }
    }
}
