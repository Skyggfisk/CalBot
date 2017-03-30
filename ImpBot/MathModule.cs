using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpBot
{
    public class MathModule : ModuleBase
    {
        /*
         * Can't do stuff like: !imp math 30+42-(2^1), 
         * as this will require a parsing engine, taking in the input parameter and determine operations etc.
         * This would also require a ton of RegEx and sanitation of input simply to insure malicuous/faulty operations or code injection don't occur.
         * Or, well, not that it can't be done... it's just really, really hard and labour-intensive. 
         */

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

        [Command("math"), Summary("Compute input string. Eg: '40+3^2'.")]
        public async Task DoMath(string input)
        {
            DataTable dt = new DataTable();
            var v = dt.Compute(input,"");
            await ReplyAsync(""+v);
        }
    }
}
