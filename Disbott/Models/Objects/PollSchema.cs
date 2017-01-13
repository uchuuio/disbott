using System;
using System.Collections.Generic;

namespace Disbott.Models.Objects
{
    /// <summary>
    /// Object to hold the polls, is stored in the db
    /// </summary>
    public class PollSchema
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }
        public string Owner { get; set; }
        public bool IsRunning { get; set; }
        public List<string> UsersVoted { get; set; }
        public DateTime Time { get; set; }
    }
}
