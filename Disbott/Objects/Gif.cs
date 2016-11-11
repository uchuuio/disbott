using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disbott.Objects
{
    public class Gif
    {
        public Dictionary<string, string> data { get; set; }
    }

    public class GifSource
    {
        public string Type { get; set; }
        public string Source { get; set; }
    }
}
