﻿using System;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using LiteDB;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disbott.Modules
{
    public class MessageCountSchema
    {
        public ulong Id { get; set; }
        public ulong Messages { get; set; }
    }

    [Module]
    public class MessageCount
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

        [Command("message-count"), Description("Displays Current Message Count for Current or Another Discord User")]
        public async Task Messagecount(IUserMessage msg)
        {
            using (var db = new LiteDatabase(@"MessageCount.db"))
            {
                var totalMessages = db.GetCollection<MessageCountSchema>("totalmessages");

                var getUserMessages = totalMessages.Find(x => x.Id.Equals(msg.Author.Id));
                var users = getUserMessages as MessageCountSchema[] ?? getUserMessages.ToArray();
                if (users.Any())
                {
                    var user = users[0];
                    await msg.Channel.SendMessageAsync(MentionUtils.MentionUser(user.Id) + " has posted " + user.Messages + " messages");
                }
                else
                {
                    await msg.Channel.SendMessageAsync("wow this user has posted no messages, incredible lurking");
                }

            }
        }
    }
}
