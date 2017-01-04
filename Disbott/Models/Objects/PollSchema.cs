using System;

namespace Disbott.Models.Objects
{
    public class PollSchema
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }
        public string Owner { get; set; }
        public bool IsRunning { get; set; }
        public string[] UsersVoted { get; set; }
        public DateTime Time { get; set; }
    }
}
