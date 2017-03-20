using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpBot
{
    public class GamesModule : ModuleBase
    {
        [Command("roll"), Summary("Roll the dice with input floor and roof")]
        public async Task RollDice([Summary("The floor of the dice")] int floor, [Summary("The roof of the dice")] int roof)
        {
            // Determine the result
            //int result = 0;
            Random rand = new Random();
            int result = rand.Next(floor, roof);

            // Write the result to chat
            await ReplyAsync($"Result: {result}, floor: {floor}, roof: {roof}");
        }

        [Command("roll"), Summary("Roll the dice with predetermined floor and roof")]
        public async Task RollDice()
        {
            Random rand = new Random();
            int result = rand.Next(0, 100);

            await ReplyAsync($"Standard roll result: {result}");
        }
    }
}
