using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

namespace ImpBot
{
    class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private DependencyMap map;

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            string token = "Mjc5MzY4MzkzOTczNDMyMzIx.C356Ag.JedWUMyPF2tGY4lOPX0vc3KOwgE";

            map = new DependencyMap();

            await InstallCommands();
            await client.LoginAsync(TokenType.Bot, token);
            await client.ConnectAsync();
            await Task.Delay(-1);
        }
        public async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task CreateCommands()
        {
            client.MessageReceived += HandleCommand;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task HandleCommand(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;
            // Create a Command Context
            var context = new CommandContext(client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed succesfully)
            var result = await commands.ExecuteAsync(context, argPos, map);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }







        //// Convert our sync-main to an async main method
        //static void Main(string[] args) => new Program().Run().GetAwaiter().GetResult();

        //// Create a DiscordClient with WebSocket support
        //private DiscordSocketClient client;

        //public async Task Run()
        //{
        //    client = new DiscordSocketClient();

        //    // Place the token of your bot account here
        //    string token = "Mjc5MzY4MzkzOTczNDMyMzIx.C356Ag.JedWUMyPF2tGY4lOPX0vc3KOwgE";

        //    // Hook into the MessageReceived event on DiscordSocketClient
        //    client.MessageReceived += async (message) =>
        //    {   // Check to see if the Message Content is "!ping"
        //        if (message.Content == "!ping")
        //            // Send 'pong' back to the channel the message was sent in
        //            await message.Channel.SendMessageAsync("pong");
        //    };

        //    // Configure the client to use a Bot token, and use our token
        //    await client.LoginAsync(TokenType.Bot, token);
        //    // Connect the client to Discord's gateway
        //    await client.ConnectAsync();

        //    // Block this task until the program is exited.
        //    await Task.Delay(-1);
        //}
    }
}
