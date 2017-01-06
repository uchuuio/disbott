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
        public static void MessageRecord(IUserMessage msg)
        {
            using (var db = new LiteDatabase(Constants.MessageCountPath))
            {
                var totalMessages = db.GetCollection<MessageCountSchema>("totalmessages");

                var getUserMessages = totalMessages.Find(x => x.Id.Equals(msg.Author.Id));
                var users = getUserMessages as MessageCountSchema[] ?? getUserMessages.ToArray();
                if (users.Any())
                {
                    var user = users[0];
                    var newCount = user.Messages + 1;

                    totalMessages.Update(new MessageCountSchema
                    {
                        Id = msg.Author.Id,
                        Messages = newCount
                    });
                }
                else
                {
                    totalMessages.Insert(new MessageCountSchema
                    {
                        Id = msg.Author.Id,
                        Messages = 1
                    });
                }
            }
        }

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
