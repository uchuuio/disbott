namespace Disbott.Models.Objects
{
    /// <summary>
    /// Lol object contains Discord ID and Summoner ID not stored in db
    /// </summary>
    public class LoLSummoner
    {
        public ulong Id { get; set; }
        public ulong DiscordID { get; set; }
        public int SummonerID { get; set; }
    }
}
