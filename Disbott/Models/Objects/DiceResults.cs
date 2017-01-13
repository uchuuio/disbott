namespace Disbott.Models.Objects
{
    /// <summary>
    /// Dice object not stored in db
    /// </summary>
    public class DiceResults
    {
        public string Results { get; set; }
        public int Total { get; set; }

        public DiceResults(string results, int total)
        {
            Total = total;
            Results = results;
        }
    }
}
