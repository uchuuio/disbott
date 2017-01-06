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
        /// <summary>
        /// Method to add a new reminder to the db
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public static bool AddRemindMeHistory(DateTime time, string note, string name = "Admin")
        {
            using (var db = new LiteDatabase(Constants.remindMePath))
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

        /// <summary>
        /// Method to display all the current active reminders
        /// </summary>
        /// <returns></returns>
        public static string GetReminders()
        {
            using (var db = new LiteDatabase(Constants.remindMePath))
            {
                string reminderHistory = "";
                var reminders = db.GetCollection<RemindMeSchema>("remindme");

                var result = reminders.FindAll();

                var allReminders = result as RemindMeSchema[] ?? result.ToArray();

                foreach (var reminder in allReminders)
                {
                    reminderHistory += $"{reminder.Id}, {reminder.Name}, {reminder.TimeDate}, {reminder.Note} \r\n";
                }

                return reminderHistory;
            }
        }

        public static string GetMyReminders(string userId)
        {
            using (var db = new LiteDatabase(Constants.remindMePath))
            {
                string reminderHistory = "";
                var reminders = db.GetCollection<RemindMeSchema>("remindme");

                var result = reminders.Find(x => x.Name.Equals(userId));

                var allReminders = result as RemindMeSchema[] ?? result.ToArray();

                foreach (var reminder in allReminders)
                {
                    reminderHistory += $"{reminder.Id}, {reminder.Name}, {reminder.TimeDate}, {reminder.Note} \r\n";
                }

                return reminderHistory;
            }
        }

        /// <summary>
        /// Method to manually remove a reminder from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteReminder(int id, string userID)
        {
            using (var db = new LiteDatabase(Constants.remindMePath))
            {
                // Open up the db
                var reminders = db.GetCollection<RemindMeSchema>("remindme");
                if (userID == "Admin2")
                {
                    reminders.Delete(x => x.Id.Equals(id));
                    return true;
                }
                else
                {
                    var result = reminders.Find(x => x.Id.Equals(id));
                    var allReminders = result as RemindMeSchema[] ?? result.ToArray();

                    if (allReminders[0].Name == userID)
                    {
                        reminders.Delete(x => x.Id.Equals(id));
                        return true;
                    }
                    //Delete the item with passed in ID
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Method to automatically remove a reminder from the db when it ends
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static bool DeleteReminderEnd(string note)
        {
            using (var db = new LiteDatabase(Constants.remindMePath))
            {
                // Open up the db
                var reminders = db.GetCollection<RemindMeSchema>("remindme");

                //Delete the item with passed in ID
                reminders.Delete(x => x.Note.Equals(note));

                return true;
            }
        }

        /// <summary>
        /// This is mainly for veryfying if the reminder should pop or not depending on wether it exists in the db
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static bool FindReminder(string note)
        {
            using (var db = new LiteDatabase(Constants.remindMePath))
            {
                // Open up the db
                var reminders = db.GetCollection<RemindMeSchema>("remindme");

                var result = reminders.Find(x => x.Note.Equals(note));

                var allReminders = result as RemindMeSchema[] ?? result.ToArray();

                try
                {
                    string answer = allReminders[0].Note;
                    return true;
                }
                catch(IndexOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    return false;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
    }
}
