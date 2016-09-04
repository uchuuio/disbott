using System;
using Discord;
using Disbott.Modules;

class Program
{
    static void Main(string[] args) => new Program().Start();

    private DiscordClient _client;

    public void Start()
    {
        _client = new DiscordClient();

        _client.MessageReceived += async (s, e) =>
        {
            Console.WriteLine(e.Message.Text);
            if (!e.Message.IsAuthor && e.Message.IsMentioningMe())
            {
                var botUsername = e.Server.CurrentUser.Name;
                var message = e.Message.Text.Replace("@"+botUsername+" ", "");
                try
                {
                    await utils.Ping(e, message);
                }

                catch
                {

                }
            }
             // await e.Channel.SendMessage(e.Message.Text);
        };

        _client.ExecuteAndWait(async () => {
            await _client.Connect("MTMxMDk0MzY0NjEyMDY3MzI4.Cq3bPQ.9lg-kJXG609wmychtrZAjK3sjYc");
        });
    }
}