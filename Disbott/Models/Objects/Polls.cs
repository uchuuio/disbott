namespace Disbott.Models.Objects
{
    public class Polls
    {
        public int Id { get; set; }
        public string PollName { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }
        public string Owner { get; set; }
        public string Message { get; set; }
    }
}
