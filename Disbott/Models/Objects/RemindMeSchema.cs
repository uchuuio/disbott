using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disbott.Models.Objects
{
    /// <summary>
    /// Object to store reminders, is stored in the db
    /// </summary>
    public class RemindMeSchema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeDate { get; set; }
        public string Note { get; set; }
    }
}
