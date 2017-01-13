using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disbott.Properties;
using Octokit;

namespace Disbott.Controllers
{
    public static class UtilsController
    {
        private static string[] coinFlipCommands = {"__**Coin flip module commands**__",Resources.Command_CoinFlip_Flip};
        private static string[] giphyCommands = {"__**Giphy module commands**__",Resources.Command_Giphy_Flip};
        private static string[] lolCommands = {"__**League of Legends module commands**__",Resources.Comand_Lol_SetSummoner,Resources.Command_Lol_Ranked,Resources.Command_Lol_CurrentGame };
        private static string[] messageCountCommands = {"__**Message count module commands**__",Resources.Command_Message_Count_Count };
        private static string[] pollCommands = {"__**Poll module commands**__",Resources.Command_Poll_NewPoll,Resources.Command_Poll_VotePoll,Resources.Command_Poll_Current,Resources.Command_Poll_Delete,Resources.Command_Poll_DeleteAll };
        private static string[] quotesCommands = {"__**Quotes module commands**__",Resources.Command_Quotes_AddQuote,Resources.Command_Quote_Quote,Resources.Command_Quote_Delete };
        private static string[] remindmeCommands = {"__**Reminders module commands**__",Resources.Command_Remindme_RemindIn,Resources.Command_Remindme_RemindThen,Resources.Command_Remindme_MyReminders,Resources.Command_Remindme_DeleteReminder,Resources.Command_Remindme_RemindThenAll,Resources.Command_Remindme_RemindInAll,Resources.Command_Remindme_AllReminders,Resources.Command_Remindme_AdDeleteReminder };
        private static string[] rollCommands = {"__**Dice roll module commands**__",Resources.Command_Roll_Roll };
        private static string[] twitterCommands = {"__**Twitter module Commands**__",Resources.Command_Twitter_Tweet,Resources.Command_Twitter_RandomTweet,Resources.Command_Twitter_Headline,Resources.Command_Twitter_gazo };
        private static string[] utilsCommands = {"__**Utils module Commands**__",Resources.Command_Utils_Ping,Resources.Command_Utils_Info,Resources.Command_Utils_About,Resources.Command_Utils_Kill,Resources.Command_Utils_Modules,Resources.Command_Utils_Help };

        public static async Task<string[]> getReleaseData()
        {
            var client = new GitHubClient(new ProductHeaderValue("Disbott"));
            var releases = await client.Repository.Release.GetAll("uchuuio", "disbott");
            var latestRelease = releases[0];

            string[] releaseData =
            {
                latestRelease.Name,
                latestRelease.HtmlUrl
            };
            return releaseData;
        }

        public static string ShowHelp(string moduleName)
        {
            if (moduleName == "coinflip")
                return string.Join("\r", coinFlipCommands);
            else if (moduleName == "leagueoflegends")
                return string.Join("\r", lolCommands);
            else if (moduleName == "giphy")
                return string.Join("\r", giphyCommands);
            else if (moduleName == "messagecount")
                return string.Join("\r", messageCountCommands);
            else if (moduleName == "polls")
                return string.Join("\r", pollCommands);
            else if (moduleName == "quotes")
                return string.Join("\r", quotesCommands);
            else if (moduleName == "reminders")
                return string.Join("\r", remindmeCommands);
            else if (moduleName == "roll")
                return string.Join("\r", rollCommands);
            else if (moduleName == "twitter")
                return string.Join("\r", twitterCommands);
            else if (moduleName == null)
                return string.Join("\r", utilsCommands);
            else
            {
                return "module does not exist";
            }
        }
    }
}
