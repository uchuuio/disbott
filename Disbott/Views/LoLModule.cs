using System;
using System.Linq;
using System.Threading.Tasks;

using LiteDB;
using RiotSharp;
using Discord.Commands;
using Tweetinvi.Core.Extensions;

using Disbott.Controllers;
using Disbott.Models.Objects;

namespace Disbott.Views
{
    [Name("LoL")]
    public class LoLModule : ModuleBase
    {
        [Command("set-summoner")]
        [Remarks("Links the specified summoner to your discord account")]
        public async Task SetSummoner([Summary("Summoner name")] string summonerName)
        {
            var msg = Context.Message;
            try
            {
                var summonerDetails = LoLController.GetSummonerData(summonerName);

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

                    var summonerApi = LoLController.GetSummonerData(discordSummoner.SummonerID.ToString());
                    var message = LoLController.GetRankedStats(summonerApi);
                    await ReplyAsync(message);
                }
            }
            else
            {
                var summonerApi = LoLController.GetSummonerData(summonerName);
                var message = LoLController.GetRankedStats(summonerApi);
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

                    var summonerApi = LoLController.GetSummonerData(discordSummoner.SummonerID.ToString());
                    var message = LoLController.GetCurrentGame(summonerApi);
                    await ReplyAsync(message);
                }
            }
            else
            {
                var summonerApi = LoLController.GetSummonerData(summonerName);
                var message = LoLController.GetCurrentGame(summonerApi);
                await ReplyAsync(message);
            }
        }
    }
}
