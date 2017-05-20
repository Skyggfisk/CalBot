using System.Threading.Tasks;
using Discord.Commands;
using info.lundin.math;

namespace ImpBot.Modules.Math
{
    public class MathModule : ModuleBase
    {
        /*
         * Can't do stuff like: !imp math 30+42-(2^1), 
         * as this will require a parsing engine, taking in the input parameter and determine operations etc.
         * This would also require a ton of RegEx and sanitation of input simply to insure malicuous/faulty operations or code injection don't occur.
         * Or, well, not that it can't be done... it's just really, really hard and labour-intensive. AKA I don't want to.
         * Edit(18-05-2017):
         * Using lundin.math dll parser from NuGet.
         * It's not super great, but it does the most of the basics. Just be explicit with it.
         */

        [Command("math"), Summary("Evaluates an input string as a mathematical expression")]
        [Alias("m", "domath", "eval")]
        public async Task EvalString(params string[] exp)
        {
            var exp1 = string.Join("", exp);
            double output;
            switch (exp1.ToLower().Trim())
            {
                case "e":
                    output = System.Math.E;
                    break;
                case "tau":
                    output = System.Math.PI * 2;
                    break;
                default:
                    var parser = new ExpressionParser();
                    output = parser.Parse(exp1);
                    break;
            }
            await ReplyAsync("" + output);

        }

        #region Old hacky methods
        //[Command("math"), Summary("Compute input string. Eg: '40+3^2'.")]
        //public async Task DoMath(string input)
        //{
        //    var dataTable = new DataTable();
        //    var output = dataTable.Compute(input, "");
        //    await ReplyAsync("" + output);
        //}

        //[Command("add")]
        //[Alias("addition", "plus", "sum")]
        //public async Task Add([Summary("The numbers to add")] params int[] values)
        //{
        //    await ReplyAsync($"Result: {values.Sum()}");
        //}

        //[Command("subtract")]
        //[Alias("subtraction", "sub", "minus", "difference", "diff")]
        //public async Task Subtract([Summary("The numbers to subtract")] params int[] values)
        //{
        //    var difference = values.Aggregate(0, (current, t) => current - t);
        //    await ReplyAsync($"Result: {difference}");
        //}


        //[Command("square"), Summary("Squares a number.")]
        //[Alias("squareroot", "sq")]
        //public async Task Square([Summary("The number to square.")] int num)
        //{
        //    await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
        //}

        #endregion
    }
}
