using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ImpBot.Modules.Games
{
	public class GamesModule : ModuleBase
    {
        [Command("roll"), Summary("Roll the dice with input floor and roof")]
        public async Task RollDice([Summary("The floor of the dice")] int floor, [Summary("The roof of the dice")] int roof)
        {
            // Determine the result
            var rand = new Random();
            var result = rand.Next(floor, roof);

            // Write the result to chat
            await ReplyAsync($"{Context.User.Username} rolled: {result}");
        }

        [Command("roll"), Summary("Roll the dice with predetermined floor and roof")]
        public async Task RollDice()
        {
            var rand = new Random();
            var result = rand.Next(0, 100);

            await ReplyAsync($"{Context.User.Username} rolled: {result}");
        }

        // TODO: Needs some way to stop the countdown. Add cancellationToken?
        [Command("countdown"), Summary("Count down from specified integer")]
        [Alias("cd", "countfrom")]
        public async Task CountDown([Summary("The integer to count down from")] int countFrom)
        {
            await ReplyAsync($"Starting countdown from {countFrom}.\nCalcifer unavailable until countdown has completed.");
            while (countFrom >= 0)
            {
                await ReplyAsync("\t" + countFrom);
                countFrom--;
                await Task.Delay(1000);
            }
            await ReplyAsync("Finished count down.");
        }

        [Command("join")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            if (channel == null)
            {
                await ReplyAsync("Must be in a channel to join.");
                return;
            }



            var audioClient = await channel.ConnectAsync();

            await ReplyAsync("");
        }

        [Command("leave")]
        public async Task LeaveChannel()
        {

            await ReplyAsync("");
        }

        [Command("dr")]
        public async Task StreamRadio()
        {

            await ReplyAsync("");
        }
    }
}
