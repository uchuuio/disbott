using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disbott.Models.Objects;

namespace Disbott.Controllers
{
    public class RemindMeController
    {
        public static bool AddRemindMeHistory(string name, string time, string note)
        {
            using (var db = new LiteDatabase(@"remindme.db"))
            {
                var quotes = db.GetCollection<RemindMeSchema>("remindme");

                var newRemindMe = new RemindMeSchema
                {
                    Name = name,
                    TimeDate = time,
                    Note = note
                };

                quotes.Insert(newRemindMe);
            }

            return true;
        }
    }
}
