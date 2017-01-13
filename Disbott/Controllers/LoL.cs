using System;
using RiotSharp;

namespace Disbott.Controllers
{
    public static class LoLController
    {
        /// <summary>
        /// Calls the Lol api for the summonername passed in
        /// </summary>
        /// <param name="summonerName"></param>
        /// <returns></returns>
        public static dynamic GetSummonerData(string summonerName)
        {
            int summonerId;
            var api = RiotApi.GetInstance(Environment.GetEnvironmentVariable("lol_api_key"));
            if (int.TryParse(summonerName, out summonerId))
            {
                return api.GetSummoner(Region.euw, summonerId);
            }
            return api.GetSummoner(Region.euw, summonerName);
        }

        /// <summary>
        /// Calls the lol api for ranked stats on the summoner passed in
        /// </summary>
        /// <param name="summonerApi"></param>
        /// <returns></returns>
        public static string GetRankedStats(dynamic summonerApi)
        {
            var message = "The Ranked Stats for " + summonerApi.Name + " are as follows:\r\n";

            var rankedLeagueData = summonerApi.GetLeagues();
            Console.WriteLine(rankedLeagueData);
            foreach (var league in rankedLeagueData)
            {
                if (league.Queue.Equals(Queue.RankedSolo5x5))
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

        /// <summary>
        /// Calls the lol api for the current game of the user passed in
        /// </summary>
        /// <param name="summonerApi"></param>
        /// <returns></returns>
        public static string GetCurrentGame(dynamic summonerApi)
        {
            var api = RiotApi.GetInstance(Environment.GetEnvironmentVariable("lol_api_key"));
            var currentGameData = api.GetCurrentGame((Platform)Region.euw, summonerApi.Id);

            Console.WriteLine(currentGameData);
            //var message = summonerApi.Name + " is currently in a game as " + Champion + " and is " + Current Stats + " as of " + time;

            return "current";
        }
    }
}
