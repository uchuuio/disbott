using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

using Discord;
using Discord.Commands;

using Disbott.Models.Objects;

namespace Disbott.Views
{
    [Name("Message Count")]
    public class MessageCountModule : ModuleBase
    {
        [Description("Adds message to the user's total message count")]
        public static void MessageRecord(IUserMessage msg)
        {
            using (var db = new LiteDatabase(@"MessageCount.db"))
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

        [Command("message-count")]
        [Remarks("Displays Current Message Count for Current or Another Discord User")]
        public async Task Messagecount()
        {
            var msg = Context.Message;
            using (var db = new LiteDatabase(@"MessageCount.db"))
            {
                var totalMessages = db.GetCollection<MessageCountSchema>("totalmessages");

                var getUserMessages = totalMessages.Find(x => x.Id.Equals(msg.Author.Id));
                var users = getUserMessages as MessageCountSchema[] ?? getUserMessages.ToArray();
                if (users.Any())
                {
                    var user = users[0];
                    await ReplyAsync(MentionUtils.MentionUser(user.Id) + " has posted " + user.Messages + " messages");
                }
                else
                {
                    await ReplyAsync("wow this user has posted no messages, incredible lurking");
                }
            }
        }
    }
}
