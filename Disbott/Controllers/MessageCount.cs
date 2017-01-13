using Disbott.Models.Objects;
using Disbott.Properties;
using Discord;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disbott.Controllers
{
    public static class MessageCount
    {

        /// <summary>
        /// Records the messages said by a user and puts it into a db
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool MessageRecord(ulong msg)
        {
            using (var db = new LiteDatabase(Constants.MessageCountPath))
            {
                var totalMessages = db.GetCollection<MessageCountSchema>("totalmessages");

                var getUserMessages = totalMessages.Find(x => x.Id.Equals(msg));
                var users = getUserMessages as MessageCountSchema[] ?? getUserMessages.ToArray();
                if (users.Any())
                {
                    var user = users[0];
                    var newCount = user.Messages + 1;

                    totalMessages.Update(new MessageCountSchema
                    {
                        Id = msg,
                        Messages = newCount
                    });
                }
                else
                {
                    totalMessages.Insert(new MessageCountSchema
                    {
                        Id = msg,
                        Messages = 1
                    });
                }
                return true;
            }
        }

        /// <summary>
        /// Gets the messages from the messages db
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string GetMessages(ulong msg)
        {
            using (var db = new LiteDatabase(Constants.MessageCountPath))
            {
                var totalMessages = db.GetCollection<MessageCountSchema>("totalmessages");

                var getUserMessages = totalMessages.FindOne(x => x.Id.Equals(msg));

                return string.Format(Resources.response_Total_Messages, MentionUtils.MentionUser(getUserMessages.Id), getUserMessages.Messages);
            }
        }
    }
}
