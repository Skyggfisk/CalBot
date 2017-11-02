﻿using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

namespace ImpBot
{
    public class Program
    {
        private CommandService _commands;
        private DiscordSocketClient _client;
        private DependencyMap _map;

        private static void Main() => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _map = new DependencyMap();

            const string token = "MzM0ODM0OTI4NTkwMjU4MTc2.DEg_Cw.RQv_nXrGIzBmkTbqWaHql0ljyoA";

            await InstallCommands();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

#pragma warning disable 4014
            _client.SetGameAsync("with fire");
#pragma warning restore 4014

            await Task.Delay(-1);
        }
        public async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task CreateCommands()
        {
            _client.MessageReceived += HandleCommand;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            var argPos = 0;
            // Determine if the message is a command, based on if it starts with 'c!' or a mention prefix
            if (!(message.HasStringPrefix("c!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            // Create a Command Context
            var context = new CommandContext(_client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed succesfully)
            var result = await _commands.ExecuteAsync(context, argPos, _map);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}
