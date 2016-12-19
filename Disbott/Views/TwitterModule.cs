using System.Threading.Tasks;

using Discord.Commands;
using Disbott.Properties;

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
            var tweetMsg = TwitterController.GetUsersLatestTweet("guardian");
            await ReplyAsync(tweetMsg);
        }

        [Command("tweet")]
        [Remarks("Gets the latest tweet from specified Twitter Account")]
        public async Task Tweet([Remainder]string twitterAccount)
        {
            var tweetMsg = TwitterController.GetUsersLatestTweet(twitterAccount);
            await ReplyAsync(string.Format(Resources.response_Twitter_User, twitterAccount,tweetMsg));
        }

        [Command("random-tweet")]
        [Remarks("Gets a random tweet from the last 40 tweets on the specified Twitter Account")]
        public async Task RandomTweet([Remainder]string twitterAccount)
        {
            var tweetMsg = TwitterController.GetUsersRandomTweet(twitterAccount);
            await ReplyAsync(string.Format(Resources.response_Twitter_User, twitterAccount, tweetMsg));
        }

        [Command("gazo")]
        [Remarks("NSFW, gets a random image from the last 40 tweets on @idol_gazo twitter account")]
        public async Task Gazo()
        {
            var tweetMsg = TwitterController.GetUsersRandomTweet("idol_gazo");
            await ReplyAsync(tweetMsg);
        }
    }
}
