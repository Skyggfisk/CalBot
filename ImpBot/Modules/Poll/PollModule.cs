using Discord.Commands;
using System.Threading.Tasks;

namespace ImpBot.Modules.Poll
{
    public class PollModule : ModuleBase
    {
        /* Used to start a poll with a specified name.
         * Count the votes cast, and broadcast those at the end of the poll.
         * Polls to be either timed or terminated by creator at call.
         * 
         * Ideally only 1 poll should be active per channel.
         * 
         * Until more advanced polls are available, it should only be possible to vote once per poll, per user.
         */

        [Command("PollTest")]
        public async Task PollTest()
        {
            await ReplyAsync("test from poll module");
        }

        // TODO: Print out the pollItems nicely. Needs a way to set amount of time.
        [Command("Poll")]
        public async Task StartPoll(string pollName, string[] pollItems)
        {
            var poll = new PollModel();
            foreach (var itemToAdd in pollItems)
            {
                var v = new PollItem { ItemName = itemToAdd, ItemVotes = 0 };
                poll.PollItems.Add(v);
            }
            await ReplyAsync($"Created poll named: {pollName.Trim()} with items: \n ");
        }

        // TODO: Stuff like naming and other fiddly parameters
        // cast a vote on a poll
        // Example: !imp vote ChangeGuildName? yes
        [Command("Vote")]
        public async Task Vote(string pollName, string itemName)
        {

            await ReplyAsync("");
        }

    }
}
