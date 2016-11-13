using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disbott.Models.Objects
{
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
