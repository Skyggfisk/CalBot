using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace ImpBot
{
    class Program
    {
        // Convert our sync-main to an async main method
        static void Main(string[] args) => new Program().Run().GetAwaiter().GetResult();

        // Create a DiscordClient with WebSocket support
        private DiscordSocketClient client;

        public async Task Run()
        {
            client = new DiscordSocketClient();

            // Place the token of your bot account here
            string token = "Mjc5MzY4MzkzOTczNDMyMzIx.C356Ag.JedWUMyPF2tGY4lOPX0vc3KOwgE";

            // Hook into the MessageReceived event on DiscordSocketClient
            client.MessageReceived += async (message) =>
            {   // Check to see if the Message Content is "!ping"
                if (message.Content == "!ping")
                    // Send 'pong' back to the channel the message was sent in
                    await message.Channel.SendMessageAsync("pong");
            };

            // Configure the client to use a Bot token, and use our token
            await client.LoginAsync(TokenType.Bot, token);
            // Connect the client to Discord's gateway
            await client.ConnectAsync();

            // Block this task until the program is exited.
            await Task.Delay(-1);
        }
    }
}
