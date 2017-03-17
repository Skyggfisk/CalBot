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
    // Create a module with the 'imp' prefix
    [Group("imp")]
    public class ImpCommandModule : ModuleBase
    {
        PathsAndTexts pat = new PathsAndTexts();
       
        [Command("help"), Summary("Get help and general guidelines for bot usage"), Alias("info", "how")]
        public async Task Help()
        {
            string helptext = System.IO.File.ReadAllText(@"Texts/ImpHelp.txt");
            await ReplyAsync(helptext);
        }

        [Command("weather")]
        public async Task Weather([Summary("City input parameter")] string city)
        {
            if(string.IsNullOrWhiteSpace(city))
            {
                await ReplyAsync("Hey! You gotta give me something to work with here!");
            }
            else
            {
            // Search openweathermap.org
            string response;
            using (var http = new HttpClient())
                response = await http.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=d8cd7126962561e2317ae72e2c231cc8&units=metric")
                        .ConfigureAwait(false);

                var data = JsonConvert.DeserializeObject<WeatherData>(response);

                await ReplyAsync(pat.RandomAttackText());
                await ReplyAsync("```Markdown" + $"\nLocation: {data.name}, {data.sys.country}" + $"\nLat/Lon: {data.coord.lat}, {data.coord.lon}" + $"\n{data.wind.speed} m/s" + "```");
                

                //var embed = new EmbedBuilder()
                //    .AddField(fb => fb.WithName("🌍 " + Format.Bold(GetText("location"))).WithValue($"[{data.name + ", " + data.sys.country}](https://openweathermap.org/city/{data.id})").WithIsInline(true))
                //    .AddField(fb => fb.WithName("📏 " + Format.Bold(GetText("latlong"))).WithValue($"{data.coord.lat}, {data.coord.lon}").WithIsInline(true))
                //    .AddField(fb => fb.WithName("☁ " + Format.Bold(GetText("condition"))).WithValue(string.Join(", ", data.weather.Select(w => w.main))).WithIsInline(true))
                //    .AddField(fb => fb.WithName("😓 " + Format.Bold(GetText("humidity"))).WithValue($"{data.main.humidity}%").WithIsInline(true))
                //    .AddField(fb => fb.WithName("💨 " + Format.Bold(GetText("wind_speed"))).WithValue(data.wind.speed + " m/s").WithIsInline(true))
                //    .AddField(fb => fb.WithName("🌡 " + Format.Bold(GetText("temperature"))).WithValue(data.main.temp + "°C").WithIsInline(true))
                //    .AddField(fb => fb.WithName("🔆 " + Format.Bold(GetText("min_max"))).WithValue($"{data.main.temp_min}°C - {data.main.temp_max}°C").WithIsInline(true))
                //    .AddField(fb => fb.WithName("🌄 " + Format.Bold(GetText("sunrise"))).WithValue($"{data.sys.sunrise.ToUnixTimestamp():HH:mm} UTC").WithIsInline(true))
                //    .AddField(fb => fb.WithName("🌇 " + Format.Bold(GetText("sunset"))).WithValue($"{data.sys.sunset.ToUnixTimestamp():HH:mm} UTC").WithIsInline(true))
                //    .WithOkColor()
                //.WithFooter(efb => efb.WithText("Powered by openweathermap.org").WithIconUrl($"http://openweathermap.org/img/w/{data.weather[0].icon}.png"));
                //await Context.Channel.EmbedAsync(embed).ConfigureAwait(false);

            }
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

        [Command("greet"), Summary("Greets a greeter"), Alias("hi", "hello", "greetings", "hey")]
        public async Task Greet()
        {
            IUser user = Context.Message.Author;
            await ReplyAsync($"Hello {user.Username}!");
        }

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
