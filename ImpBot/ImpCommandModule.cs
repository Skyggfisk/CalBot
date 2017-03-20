using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Net.Http;
using Newtonsoft.Json;

namespace ImpBot
{
    public class ImpCommandModule : ModuleBase
    {
        PathsAndTexts pat = new PathsAndTexts();
       
        [Command("help"), Summary("Get help and general guidelines for bot usage"), Alias("info", "how")]
        public async Task Help()
        {
            string helptext = System.IO.File.ReadAllText(@"Texts/ImpHelp.txt");
            await ReplyAsync(helptext);
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

        

        // !say hello -> hello
        [Command("say"), Summary("Echos a message.")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            await ReplyAsync(echo);
        }

        [Command("greet"), Summary("Greets a greeter"), Alias("hi", "hello", "greetings", "hey")]
        public async Task Greet()
        {
            IUser user = Context.Message.Author;
            await ReplyAsync($"Hello {user.Username}!");
        }
    }
}
