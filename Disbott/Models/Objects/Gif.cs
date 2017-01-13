using System.Collections.Generic;

namespace Disbott.Models.Objects
{
    /// <summary>
    /// Gif onject. Contains 2 strings, not stored in db
    /// </summary>
    public class Gif
    {
        public Dictionary<string, string> data { get; set; }
    }
}
