using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ImpBot
{
    // Create a module with the 'imp' prefix
    [Group("imp")]
    public class ImpCommandModule : ModuleBase
    {
        PathsAndTexts pat = new PathsAndTexts();
       
        [Command("help"), Summary("Get help and general guidelines for bot usage")]
        public async Task Help()
        {
            await ReplyAsync($"Need some help text here...");
        }

        // Shake the imp to recieve an answer to your questions
        [Command("shake"), Summary("Shake the poor imp. Why would you do this?")]
        public async Task Shake()
        {
            await ReplyAsync(pat.ShakeText());
        }

        // Summons a new imp by changing its display name and avatar
        [Command("resummon"), Summary("Summons a new imp"), Alias("sacrifice", "sac")]
        public async Task Resummon()
        {
            //Context.User.
            await ReplyAsync("\\*Demonic cackle*");
        }

        [Command("image"), Summary("Posts an image to chat."), Alias("img", "pic", "picture", "meme")]
        public async Task PostImage()
        {
            await Context.Channel.SendFileAsync(pat.RandomImage());
        }

        [Command("talk"), Summary("Says some random imp stuff."), Alias("joke", "silly", "imp")]
        public async Task Talk()
        {
            await ReplyAsync(pat.RandomAttackText());
        }

        // !imp square 20 -> 400
        [Command("square"), Summary("Squares a number.")]
        public async Task Square([Summary("The number to square.")] int num)
        {
            // We can also access the channel from the Command Context.
            await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
        }

        // !say hello -> hello
        [Command("say"), Summary("Echos a message.")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync(echo);
        }

        [Command("greet"), Summary("Greets a greeter"), Alias("hi", "hello", "greetings", "gr")]
        public async Task Greet()
        {
            IUser user = Context.Message.Author;
            await ReplyAsync($"Hello {user.Username}!");
        }

        //[Command("purge"), Summary("Purges a text chat"), RequireOwner]
        //public async Task Purge()
        //{
        //    IMessage[] messagesToDelete = Context.Channel.GetMessagesAsync(100);
        //    await Context.Channel.DeleteMessagesAsync(messagesToDelete);
        //}

        // !sample userinfo --> foxbot#0282
        // !sample userinfo @Khionu --> Khionu#8708
        // !sample userinfo Khionu#8708 --> Khionu#8708
        // !sample userinfo Khionu --> Khionu#8708
        // !sample userinfo 96642168176807936 --> Khionu#8708
        // !sample whois 96642168176807936 --> Khionu#8708
        [Command("userinfo"), Summary("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois", "who")]
        public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}
