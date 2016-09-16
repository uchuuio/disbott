using System;
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
        public ulong DiscordID { get; set; }
        public long SummonerID { get; set; }
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
                        DiscordID = msg.Author.Id,
                        SummonerID = summonerDetails.Id
                    };

                    if (summoners.Find(x => x.DiscordID.Equals(msg.Author.Id)).Any())
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
                    var rankedStats = api.GetStatsRanked(Region.euw, discordSummoner.SummonerID);
                }
                catch (RiotSharpException ex)
                {
                    // Handle the exception however you want.
                }
            }

            await msg.Channel.SendMessageAsync("");
        }
    }
}
