using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disbott
{
    public class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            string token = "MTMxMDk0MzY0NjEyMDY3MzI4.CrRQtg.iH_BbVcTvXActUewvyy4wEd0wxk";

            await InstallCommands();

            await client.LoginAsync(TokenType.Bot, token);
            await client.ConnectAsync();

            await ModifyStatus();

            await Task.Delay(-1);
        }

        public async Task ModifyStatus()
        {
            await (await client.GetCurrentUserAsync()).ModifyStatusAsync(x =>
            {
                x.Status = UserStatus.Idle;
                x.Game = new Game("'@disbott-dev about' for commands");
            });
        }


        public async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await commands.LoadAssembly(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(IMessage paramMessage)
        {
            // Cast paramMessage to an IUserMessage, return if the message was a System message.
            var msg = paramMessage as IUserMessage;
            if (msg == null) return;
            // Internal integer, marks where the command begins
            int argPos = 0;
            // Get the current user (used for Mention parsing)
            var currentUser = await client.GetCurrentUserAsync();
            // Determine if the message is a command, based on if it contains a mention prefix
            if (msg.HasMentionPrefix(currentUser, ref argPos))
            {
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed succesfully)
                var result = await commands.Execute(msg, argPos);
                if (!result.IsSuccess)
                    await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}