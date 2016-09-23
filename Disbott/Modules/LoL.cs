using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using LiteDB;
using RiotSharp;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Tweetinvi.Models;

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
        [Command("set-summoner"), Description("Links the specified summoner to your discord account")]
        public async Task SetSummoner(IUserMessage msg, [Summary("Summoner name")] string summonerName)
        {
            var api = RiotApi.GetInstance(ConfigurationManager.AppSettings["lol_api_key"]);

            try
            {
                var summonerDetails = api.GetSummoner(Region.euw, summonerName);

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
        public async Task Ranked(IUserMessage msg)
        {
            var api = RiotApi.GetInstance(ConfigurationManager.AppSettings["lol_api_key"]);

            using (var db = new LiteDatabase(@"LoL.db"))
            {
                var summoners = db.GetCollection<LoLSummoner>("lolsummoners");

                var summoner = summoners.Find(x => x.DiscordID.Equals(msg.Author.Id));
                var loLSummoners = summoner as LoLSummoner[] ?? summoner.ToArray();
                var discordSummoner = loLSummoners[0];

                try
                {
                    var summonerApi = api.GetSummoner(Region.euw, discordSummoner.SummonerID);

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
                            double ka = k + a;
                            double kdaVal = ka/d;
                            string kda = string.Format("{0:N2}", kdaVal);

                            message += stats.Stats.TotalSessionsWon + " wins & " + stats.Stats.TotalSessionsLost + " losses\r\n";
                            message += k + "kills " + d + "deaths " + a + "assists which is a " + kda +
                                       "kda this season";
                        }
                    }

                    await msg.Channel.SendMessageAsync(message);
                }
                catch (RiotSharpException ex)
                {
                    // Handle the exception however you want.
                }
            }
        }
    }
}
