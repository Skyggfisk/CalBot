using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ImpBot.Modules.Administration
{
    public class ManagementModule : ModuleBase
    {
        [Command("kick"), RequireBotPermission(GuildPermission.BanMembers), RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick([Summary("User to kick")] IUser userToKick)
        {
            // Won't kick channel owner or me
            if (userToKick.Id == Context.Guild.OwnerId || userToKick.Id == 112982842824196096)
            {
                await ReplyAsync("Get lost kid!");
            }
            else
            {
                await Context.Guild.AddBanAsync(userToKick);
                await ReplyAsync($"Kicked user: {userToKick.Username}#{userToKick.Discriminator}");
                await Context.Guild.RemoveBanAsync(userToKick);
            }
        }
    }
}
