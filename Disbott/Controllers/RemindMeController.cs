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
                var reminders = db.GetCollection<RemindMeSchema>("remindme");

                var newRemindMe = new RemindMeSchema
                {
                    Name = name,
                    TimeDate = time,
                    Note = note
                };

                reminders.Insert(newRemindMe);
            }

            return true;
        }

        public static string GetReminders()
        {
            using (var db = new LiteDatabase(@"remindme.db"))
            {
                string reminderHistory = "";
                var reminders = db.GetCollection<RemindMeSchema>("remindme");

                var result = reminders.FindAll();

                var allReminders = result as RemindMeSchema[] ?? result.ToArray();

                foreach (var reminder in allReminders)
                {
                    reminderHistory += $"{reminder.Name}, {reminder.TimeDate}, {reminder.Note} \r\n";
                }

                return reminderHistory;
            }
        }

        public static bool DeleteReminder(string note, string name = "Default")
        {
            using (var db = new LiteDatabase(@"remindme.db"))
            {
                var reminders = db.GetCollection<RemindMeSchema>("remindme");
                var result = reminders.Find(x => x.Note.Equals(note));

                var currentReminder = result as RemindMeSchema;

                if (name == "Default")
                {
                    reminders.Delete(x => x.Note.Equals(note));
                    return true;
                }
                else if (name == currentReminder.Name)
                {
                    reminders.Delete(x => x.Note.Equals(note));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
