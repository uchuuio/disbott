using System;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using LiteDB;
using RiotSharp;

using Discord;
using Discord.Commands;
using Tweetinvi.Core.Extensions;

namespace Disbott.Modules
{
    public class LoLSummoner
    {
        public ulong Id { get; set; }
        public ulong DiscordID { get; set; }
        public int SummonerID { get; set; }
    }

    [Module]
    public class LoL
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

        public static string getRankedStats(dynamic summonerApi)
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

        [Command("set-summoner"), Description("Links the specified summoner to your discord account")]
        public async Task SetSummoner(IUserMessage msg, [Summary("Summoner name")] string summonerName)
        {
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

                await msg.Channel.SendMessageAsync("You have now linked " + summonerName + " to your Discord Account");
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
            }
        }

        [Command("ranked"), Description("Gets the ranked stats for yourself or another discord/league account")]
        public Task Ranked(IUserMessage msg)
        {
            return Ranked(msg, null);
        }

        [Command("ranked"), Description("Gets the ranked stats for yourself or another discord/league account")]
        public async Task Ranked(IUserMessage msg, [Summary("Summoner name")] string summonerName)
        {
            if (summonerName.IsNullOrEmpty())
            {
                using (var db = new LiteDatabase(@"LoL.db"))
                {
                    var summoners = db.GetCollection<LoLSummoner>("lolsummoners");

                    var summoner = summoners.Find(x => x.DiscordID.Equals(msg.Author.Id));
                    var loLSummoners = summoner as LoLSummoner[] ?? summoner.ToArray();
                    var discordSummoner = loLSummoners[0];

                    var summonerApi = GetSummonerData(discordSummoner.SummonerID.ToString());
                    var message = getRankedStats(summonerApi);
                    await msg.Channel.SendMessageAsync(message);
                }
            }
            else
            {
                var summonerApi = GetSummonerData(summonerName);
                var message = getRankedStats(summonerApi);
                await msg.Channel.SendMessageAsync(message);
            }
        }
    }
}
