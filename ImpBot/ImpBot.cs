using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace ImpBot
{
    class ImpBot
    {
        DiscordClient discord;
        CommandService commands;
        Random rand;
        string[] images;
        string[] randomTexts;

        public ImpBot()
        {
            rand = new Random();

            #region Imp images
            images = new string[]
            {
                "Images/image1.jpg",
                "Images/image2.png",
                "Images/image3.jpg",
                "Images/image4.jpg"
            };
            #endregion

            #region Imp quotes
            randomTexts = new string[]
            {
                // attack order quotes
                "Is this REALLY necessary?!",
                "This was NOT in my contract!",
                "Can't we all just get along?",
                "Ohhhh sure, send the little guy!",

                // spell quotes
                "What's in it for me?",
                "Do I have to?!",
                "Ahh! Okay, okay, okay, okay, okay, okay!",
                "Yeah, I'll get right on it.",

                // dismissed quotes
                "You know, we've had a lot of fun together, it's been really special, but I think it's time I should start seeing other warlocks. Just a little on the side. No no no it's not you, it's not you, it's me. I just need my space, it's nobody's fault.",
                "Don't call on me, I'll call on you.",
                "Argh! I feel so used!",
                "Goodbye. Thanks.",
                "*indistinct grumbling*...I wish...*indistinct grumbling*...wish you were DEAD.",

                // misc quotes
                "What? You mean you can't kill this one by yourself?",
                "Make yourself useful and help me out on this one!",
                "Release me already, I've had enough!",
                "Alright, I'm on it! Stop yelling!",
                "No shi rakir no tiros kamil re lok ante refir shi rakir",
                "Maz ruk X rikk xi laz enkil parn zila zilthuras karkun thorje kar x zennshi"
            };
            #endregion

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterGreetingsCommand();
            RegisterImageCommand();
            RegisterRandomTextCommand();
            RegisterPurgeCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("Mjc5MzY4MzkzOTczNDMyMzIx.C356Ag.JedWUMyPF2tGY4lOPX0vc3KOwgE", TokenType.Bot);
            });

        }

        private void RegisterGreetingsCommand()
        {
            commands.CreateCommand("greet")
                .Alias(new string[] { "gr", "hi", "hello", "greetings", "hey" })
                .Description("Greets the greeter.")
                .Do(async e =>
                {
                    await e.Channel.SendMessage("Hi " + e.User.Name + "!");
                });
        }

        private void RegisterImageCommand()
        {
            commands.CreateCommand("image")
                .Alias(new string[] { "img", "picture", "pic" })
                .Description("Sends a random image to chat.")
                .Do(async (e) =>
                {
                    int randomImageIndex = rand.Next(images.Length);
                    string ImagesToPost = images[randomImageIndex];
                    await e.Channel.SendFile(ImagesToPost);
                });
        }

        private void RegisterRandomTextCommand()
        {
            commands.CreateCommand("say")
                .Do(async (e) =>
                {
                    int randomTextsIndex = rand.Next(randomTexts.Length);
                    string randomTextToPost = randomTexts[randomTextsIndex];
                    await e.Channel.SendMessage(randomTextToPost);
                });
        }

        private void RegisterPurgeCommand()
        {
            commands.CreateCommand("purge")
                .Do(async (e) =>
                {
                    if(e.User.ServerPermissions.Administrator)
                    {
                        Message[] messagesToDelete;
                        messagesToDelete = await e.Channel.DownloadMessages(100);
                        await e.Channel.DeleteMessages(messagesToDelete);
                    }
                    //else if(/*does the bot have the necessary permissions?*/)
                    //{

                    //}
                    else
                    {
                        await e.Channel.SendMessage("Nice try, kid.");
                    }
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
