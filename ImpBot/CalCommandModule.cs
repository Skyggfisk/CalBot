using System.Threading.Tasks;
using Discord.Commands;

namespace CalBot
{
    public class CalCommandModule : ModuleBase
    {
        private readonly PathsAndTexts _pat = new PathsAndTexts();

        [Command("help")]
        public async Task HelpCommand(string input)
        {
            switch (input)
            {
                case "math":
                    await ReplyAsync("NYI");
                    break;
                case "roll":
                    await ReplyAsync("NYI");
                    break;
                case "poll":
                    var pollHelp = System.IO.File.ReadAllText(@"Texts/CalHelp_Poll.txt");
                    await ReplyAsync(pollHelp);
                    break;
                case "image":
                    await ReplyAsync("NYI");
                    break;
                case "quiz":
                    await ReplyAsync("NYI");
                    break;
                case "weather":
                    var weatherHelp = System.IO.File.ReadAllText(@"Texts/CalHelp_Weather.txt");
                    await ReplyAsync(weatherHelp);
                    break;
                default:
                    await ReplyAsync($"Help for {input.ToUpper()} not found");
                    break;
            }
        }

        [Command("help"), Summary("Get help and general guidelines for bot usage"), Alias("info", "how")]
        public async Task Help()
        {
            var helptext = System.IO.File.ReadAllText(@"Texts/CalHelp.txt");
            await ReplyAsync(helptext);
        }

		// Test command for CalQuotes
		[Command("cal")]
		public async Task CalQuote()
		{
			await ReplyAsync(_pat.RandomCalciferQuote());
		}

        [Command("image"), Summary("Posts an image to chat."), Alias("img", "pic", "picture", "meme")]
        public async Task PostImage()
        {
            await Context.Channel.SendFileAsync(_pat.RandomImage(), _pat.RandomCalciferQuote());
        }

        // c!say hello -> hello
        [Command("say"), Summary("Echoes a message.")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            await ReplyAsync(echo);
        }

        [Command("greet"), Summary("Greets a greeter"), Alias("hi", "hello", "greetings", "hey")]
        public async Task Greet()
        {
            var user = Context.Message.Author;
            await ReplyAsync($"Hello {user.Username}!");
        }
    }
}
