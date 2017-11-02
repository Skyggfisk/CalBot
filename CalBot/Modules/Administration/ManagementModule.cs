using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace CalBot.Modules.Administration
{
    public class ManagementModule : ModuleBase
    {
        [Command("kick"), RequireBotPermission(GuildPermission.BanMembers), RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick([Summary("User to kick")] IUser userToKick)
        {
            // Won't kick channel owner or me
            if (userToKick.Id == Context.Guild.OwnerId || userToKick.Id == 112982842824196096)
            {
                await ReplyAsync("Nice try...");
            }
            else
            {
                await Context.Guild.AddBanAsync(userToKick);
                await ReplyAsync($"Kicked user: {userToKick.Username}#{userToKick.Discriminator}");
                await Context.Guild.RemoveBanAsync(userToKick);
            }
        }

        // TODO: purges a text chat.
        // NYI an optional argument to determine number of messages to purge.
        [Command("purge")]
        public async Task Purge()
        {
            //var messagesToDelete = Context.Channel.GetMessagesAsync(10);

            //var msgDel = messagesToDelete.ToEnumerable();
            
            //await Context.Channel.DeleteMessagesAsync(msgDel);
            await ReplyAsync("By fire be purged!");
        }
    }
}
