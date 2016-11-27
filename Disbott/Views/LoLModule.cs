using System;
using System.Linq;
using System.Threading.Tasks;

using LiteDB;
using RiotSharp;
using Discord.Commands;
using Disbott.Models.Objects;
using Tweetinvi.Core.Extensions;

namespace Disbott.Views
{
    [Name("LoL")]
    public class LoLModule : ModuleBase
    {
        public static dynamic GetSummonerData(string summonerName)
        {
            int summonerId;
            var api = RiotApi.GetInstance(ConfigurationManager.AppSettings["lol_api_key"]);
            if (int.TryParse(summonerName, out summonerId))
            {
                return api.GetSummoner(Region.euw, summonerId);
            }
            return api.GetSummoner(Region.euw, summonerName);
        }

        public static string GetRankedStats(dynamic summonerApi)
        {
            var message = "The Ranked Stats for " + summonerApi.Name + " are as follows:\r\n";

            var rankedLeagueData = summonerApi.GetLeagues();
            foreach (var league in rankedLeagueData)
            {
                if (league.Queue.Equals(RiotSharp.Queue.RankedSolo5x5))
                {
                    var tier = league.Tier;
                    foreach (var leagueEntry in league.Entries)
                    {
                        var division = leagueEntry.Division;
                        var lp = leagueEntry.LeaguePoints + "lp";

                        message += tier + " " + division + " with " +
                                   lp + "\r\n";
                    }
                }
            }

            var rankedStatsData = summonerApi.GetStatsRanked();
            foreach (var stats in rankedStatsData)
            {
                if (stats.ChampionId.Equals(0))
                {
                    double k = stats.Stats.TotalChampionKills;
                    double d = stats.Stats.TotalDeathsPerSession;
                    double a = (stats.Stats.TotalAssists / 4);
                    var ka = k + a;
                    var kdaVal = ka / d;
                    var kda = $"{kdaVal:N2}";

                    message += stats.Stats.TotalSessionsWon + " wins & " + stats.Stats.TotalSessionsLost +
                               " losses\r\n";
                    message += k + "kills " + d + "deaths " + a + "assists which is a " + kda +
                               "kda this season";
                }
            }

            return message;
        }

        public static string GetCurrentGame(dynamic summonerApi)
        {
            var api = RiotApi.GetInstance(ConfigurationManager.AppSettings["lol_api_key"]);
            var currentGameData = api.GetCurrentGame((Platform) Region.euw, summonerApi.Id);

            Console.WriteLine(currentGameData);
            //var message = summonerApi.Name + " is currently in a game as " + Champion + " and is " + Current Stats + " as of " + time;

            return "current";
        }

        [Command("set-summoner")]
        [Remarks("Links the specified summoner to your discord account")]
        public async Task SetSummoner([Summary("Summoner name")] string summonerName)
        {
            var msg = Context.Message;
            try
            {
                var summonerDetails = GetSummonerData(summonerName);

                using (var db = new LiteDatabase(@"LoL.db"))
                {
                    var summoners = db.GetCollection<LoLSummoner>("lolsummoners");

                    var summoner = new LoLSummoner
                    {
                        Id = msg.Author.Id,
                        DiscordID = msg.Author.Id,
                        SummonerID = (int)summonerDetails.Id
                    };

                    var isNew = summoners.Find(x => x.Id.Equals(msg.Author.Id));

                    if (isNew.Any())
                    {
                        summoners.Update(summoner);
                    }
                    else
                    {
                        summoners.Insert(summoner);
                    }
                }

                await ReplyAsync("You have now linked " + summonerName + " to your Discord Account");
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                Console.WriteLine(ex);
            }
        }

        [Command("ranked")]
        [Remarks("Gets the ranked stats for yourself or another discord/league account")]
        public Task Ranked()
        {
            return Ranked(null);
        }

        [Command("ranked")]
        [Remarks("Gets the ranked stats for yourself or another discord/league account")]
        public async Task Ranked([Summary("Summoner name")] string summonerName)
        {
            var msg = Context.Message;
            if (summonerName.IsNullOrEmpty())
            {
                using (var db = new LiteDatabase(@"LoL.db"))
                {
                    var summoners = db.GetCollection<LoLSummoner>("lolsummoners");

                    var summoner = summoners.Find(x => x.DiscordID.Equals(msg.Author.Id));
                    var loLSummoners = summoner as LoLSummoner[] ?? summoner.ToArray();
                    var discordSummoner = loLSummoners[0];

                    var summonerApi = GetSummonerData(discordSummoner.SummonerID.ToString());
                    var message = GetRankedStats(summonerApi);
                    await ReplyAsync(message);
                }
            }
            else
            {
                var summonerApi = GetSummonerData(summonerName);
                var message = GetRankedStats(summonerApi);
                await ReplyAsync(message);
            }
        }

        [Command("current-game")]
        [Remarks("Gets the current game for yourself or another discord/league account")]
        public Task CurrentGame()
        {
            return CurrentGame(null);
        }

        [Command("current-game")]
        [Remarks("Gets the current game for yourself or another discord/league account")]
        public async Task CurrentGame([Summary("Summoner name")] string summonerName)
        {
            var msg = Context.Message;
            if (summonerName.IsNullOrEmpty())
            {
                using (var db = new LiteDatabase(@"LoL.db"))
                {
                    var summoners = db.GetCollection<LoLSummoner>("lolsummoners");

                    var summoner = summoners.Find(x => x.DiscordID.Equals(msg.Author.Id));
                    var loLSummoners = summoner as LoLSummoner[] ?? summoner.ToArray();
                    var discordSummoner = loLSummoners[0];

                    var summonerApi = GetSummonerData(discordSummoner.SummonerID.ToString());
                    var message = GetCurrentGame(summonerApi);
                    await ReplyAsync(message);
                }
            }
            else
            {
                var summonerApi = GetSummonerData(summonerName);
                var message = GetCurrentGame(summonerApi);
                await ReplyAsync(message);
            }
        }
    }
}
