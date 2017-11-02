using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using RestSharp;

namespace ImpBot.Modules.Search
{
    public class SearchModule : ModuleBase
    {

        // Search openweathermap.org at city name
        [Command("weather")]
        public async Task Weather([Summary("City input parameter")] string city)
        {
            string response;
            using (var http = new HttpClient())
                response = await http.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=d8cd7126962561e2317ae72e2c231cc8&units=metric")
                        .ConfigureAwait(false);

            var data = JsonConvert.DeserializeObject<WeatherData>(response);

            var sunrise = new DateTime();
            sunrise = sunrise.AddSeconds(data.Sys.Sunrise);

            var sunset = new DateTime();
            sunset = sunset.AddSeconds(data.Sys.Sunset);

            var embed = new EmbedBuilder()
                .AddField(fb => fb.WithName(":earth_africa: Location:").WithValue($"{data.Name}, {data.Sys.Country}").WithIsInline(true))
                .AddField(fb => fb.WithName(":straight_ruler: Lat/Lon:").WithValue($"{data.Coord.Lat}, {data.Coord.Lon}").WithIsInline(true))
                .AddField(fb => fb.WithName(":sun_with_face: Temperature:").WithValue($"{data.Main.Temp} °C").WithIsInline(true))
                .AddField(fb => fb.WithName(":pencil: Description:").WithValue(string.Join(", ", data.Weather.Select(w => w.Main))).WithIsInline(true))
                .AddField(fb => fb.WithName(":sunflower: Min / max:").WithValue($"{data.Main.TempMin} °C / {data.Main.TempMax} °C").WithIsInline(true))
                .AddField(fb => fb.WithName(":gem: Pressure:").WithValue($"{data.Main.Pressure} hPa").WithIsInline(true))
                .AddField(fb => fb.WithName(":sweat_drops: Humidity:").WithValue($"{data.Main.Humidity} %").WithIsInline(true))
                .AddField(fb => fb.WithName(":sunrise_over_mountains: Sunrise:").WithValue($"{sunrise.Hour}:{sunrise.Minute}").WithIsInline(true))
                .AddField(fb => fb.WithName(":city_sunset: Sunset:").WithValue($"{sunset.Hour}:{sunset.Minute}").WithIsInline(true))
                .AddField(fb => fb.WithName(":dash: Wind:").WithValue($"Speed: {data.Wind.Speed} m/s \nDeg: {data.Wind.Deg} °").WithIsInline(true));
            var em = embed.Build();

            await Context.Channel.SendMessageAsync("May all your bacon burn...", embed: em);

        }

        // Search Oxford Dictionaries for definition of a given english word
        // TODO: If I feel like it. Make it iterate through the  data model and retrieve all definitions, categories, and examples etc.
        // Can probably still throw a bunch of errors... needs a smarter way of handling collections.
        [Command("define")]
        public async Task OxfordDefine(string input)
        {
            var client = new RestClient($"https://od-api.oxforddictionaries.com/api/v1/entries/en/{input.ToLower()}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("app_key", "2a4ab6b0d205145ae58bf83186cb2ac3");
            request.AddHeader("app_id", "09880ad7");
            var response = client.Execute(request);

            var data = JsonConvert.DeserializeObject<Example>(response.Content);

            var embed = new EmbedBuilder()
                 .AddField(fb => fb.WithName("Id: ").WithValue($"{data.Results[0].Id.ToUpper()}").WithIsInline(true))
                 .AddField(fb => fb.WithName("Definitions: ").WithValue($"{data.Results[0].LexicalEntries[0].Entries[0].Senses[0].Definitions[0]}"));
            if (data.Results[0].LexicalEntries[0].Entries[0].Etymologies != null)
            {
                embed.AddField(fb => fb.WithName("Etymology: ").WithValue($"{data.Results[0].LexicalEntries[0].Entries[0].Etymologies[0]}"));
            }
            var em = embed.Build();
            await ReplyAsync("Found something...", embed: em);

        }

        // display detailed user information
        [Command("userinfo"), Summary("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois", "who")]
        public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}
