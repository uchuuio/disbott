using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Disbott.Controllers;

using Disbott.Views;

namespace Disbott
{
    public class Program
    {
        private CommandService _commands;
        private DiscordSocketClient _client;

        private static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        /// <summary>
        /// Stars Disbott Running
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            #if DEBUG
            var envpath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/../variables.env";
            if (File.Exists(envpath))
            {
                Dictionary<string, string> variables = DotEnvFile.DotEnvFile.LoadFile(envpath);
                DotEnvFile.DotEnvFile.InjectIntoEnvironment(variables);
            }
            #endif

            var token = Environment.GetEnvironmentVariable("token");

            await InstallCommands();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.ConnectAsync();

            await ModifyStatus();

            await Task.Delay(-1);
        }

        /// <summary>
        /// Modifysd any initial disbott info we want
        /// </summary>
        /// <returns></returns>
        public async Task ModifyStatus()
        {
            var username = _client.CurrentUser.Username;
            await _client.SetStatusAsync(UserStatus.Online);
            await _client.SetGameAsync($"\'@{username} about\' for commands");
            //await _client.CurrentUser.ModifyAsync(x =>
            //{
            //    x.Avatar = new Image(Constants.avatarImage);
            //});
        }

        /// <summary>
        /// Adds the commands to the program
        /// </summary>
        /// <returns></returns>
        public async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// Tells disbott what to do with incoming commands
        /// </summary>
        /// <param name="messageParam"></param>
        /// <returns></returns>
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

            if (message.Author.Id != _client.CurrentUser.Id)
            {
                // MessageCount Record
                MessageCountController.MessageRecord(message.Author.Id);
            }
        }

    }
}