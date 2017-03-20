using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpBot
{
    public class MathModule : ModuleBase
    {

        [Command("add")]
        [Alias("addition", "plus")]
        public async Task Add([Summary("The numbers to add")] params int[] values)
        {
            int sum = 0;
            for(int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            await ReplyAsync($"Result: {sum}");
        }

        [Command("subtract")]
        [Alias("subtraction", "sub","minus")]
        public async Task subtract([Summary("The numbers to subtract")] params int[] values)
        {
            int sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum -= values[i];
            }
            await ReplyAsync($"Result: {sum}");
        }


        [Command("square"), Summary("Squares a number.")]
        [Alias("squareroot" , "sq")]
        public async Task Square([Summary("The number to square.")] int num)
        {
            await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
        }
    }
}
