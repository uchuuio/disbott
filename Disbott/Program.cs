using System;
using System.Threading.Tasks;
using System.Reflection;
using Disbott.Views;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disbott
{
    public class Program
    {
        private CommandService _commands;
        private DiscordSocketClient _client;

        private static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            var token = ConfigurationManager.AppSettings["token"];

            await InstallCommands();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.ConnectAsync();

            await ModifyStatus();

            await Task.Delay(-1);
        }

        public async Task ModifyStatus()
        {
            var username = _client.CurrentUser.Username;
            await _client.SetStatus(UserStatus.Idle);
            await _client.SetGame($"\'@{username} about\' for commands");
        }

        public async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }
        public async Task HandleCommand(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                // Create a Command Context
                var context = new CommandContext(_client, message);
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed succesfully)
                var result = await _commands.ExecuteAsync(context, argPos);
                if (!result.IsSuccess)
                    await message.Channel.SendMessageAsync(result.ErrorReason);
            }

            // MessageCount Record
            MessageCountModule.MessageRecord(message);
        }

    }
}