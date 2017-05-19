using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace ImpBot.Modules.Search
{
    public class SearchModule : ModuleBase
    {
        private readonly PathsAndTexts _pat = new PathsAndTexts();

        // Search openweathermap.org at city name
        [Command("weather")]
        public async Task Weather([Summary("City input parameter")] string city)
        {
            await ReplyAsync(_pat.RandomAttackText());

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
                .AddField(fb => fb.WithName(":sun_with_face: Temperature:").WithValue($"{data.Main.Temp} °C"))
                .AddField(fb => fb.WithName(":pencil: Description:").WithValue(string.Join(", ", data.Weather.Select(w => w.Main))))
                .AddField(fb => fb.WithName(":sunflower: Min / max:").WithValue($"{data.Main.TempMin} °C / {data.Main.TempMax} °C"))
                .AddField(fb => fb.WithName(":gem: Pressure:").WithValue($"{data.Main.Pressure} hPa"))
                .AddField(fb => fb.WithName(":sweat_drops: Humidity:").WithValue($"{data.Main.Humidity} %"))
                .AddField(fb => fb.WithName(":sunrise_over_mountains: Sunrise:").WithValue($"{sunrise.Hour}:{sunrise.Minute}"))
                .AddField(fb => fb.WithName(":city_sunset: Sunset:").WithValue($"{sunset.Hour}:{sunset.Minute}"))
                .AddField(fb => fb.WithName(":dash: Wind:").WithValue($"Speed: {data.Wind.Speed} m/s \nDeg: {data.Wind.Deg} °"));
            var em = embed.Build();

            await Context.Channel.SendMessageAsync("", embed: em);

        }

        [Command("userinfo"), Summary("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois", "who")]
        public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}
