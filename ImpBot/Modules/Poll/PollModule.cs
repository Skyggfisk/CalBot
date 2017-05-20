using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;

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

        // Needed to keep the polls alive.
        private static List<PollModel> _pollList = new List<PollModel>();

        [Command("PollHelp")]
        public async Task PollHelp()
        {
            await ReplyAsync("<PH> Needs some helpful text here");
        }

        // TODO: Print out the pollItems nicely. Needs a way to set amount of time.
        // TODO: Naming like "Change guild name?" - a fix could be "!imp poll name:Change guild name?"
        [Command("Poll")]
        public async Task StartPoll(string pollName, params string[] pollItems)
        {
            // Create the new poll
            var poll = new PollModel
            {
                Creator = Context.Message.Author,
                PollName = pollName,
                PollItems = new List<PollItem>(),
                IsActive = true,
                VotedUsersList = new List<IUser>()

            };

            // add pollItems to the poll
            foreach (var itemToAdd in pollItems)
            {
                var v = new PollItem { ItemName = itemToAdd, ItemVotes = 0 };
                poll.PollItems.Add(v);
            }

            _pollList.Add(poll);

            var stringBuilder = new StringBuilder();
            foreach (var x in pollItems)
            {
                stringBuilder.AppendLine(x.Trim());
            }

            //var embed = new EmbedBuilder()
            //    .AddField(fb => fb.WithName("Poll name: ").WithValue($"{poll.PollName}"))
            //    .AddField(fb => fb.WithName("Items: "))
            //    .AddField(fb => fb.WithName(stringBuilder.ToString()));
            //var em = embed.Build();

            //await ReplyAsync("", embed: em);
            await ReplyAsync($"created poll named: {poll.PollName} with options: \n{stringBuilder}");
        }

        // TODO: Stuff like naming and other fiddly parameters
        // cast a vote for an item in an active poll
        // Example: !imp vote ChangeGuildName? yes
        [Command("Vote")]
        public async Task Vote(string pollName, string itemName)
        {

            await ReplyAsync("NYI");
        }


        // TODO: End the poll and broadcast the results to the guild channel.
        [Command("EndPoll")]
        public async Task EndPoll(string pollName)
        {

            await ReplyAsync("NYI");
        }

        // TODO: Ends a poll prematurely without broadcasting results.
        [Command("CancelPoll")]
        public async Task CancelPoll(string pollName)
        {
            if (_pollList.Any(x => x.PollName == pollName))
            {
                _pollList.RemoveAll(x => x.PollName == pollName);
                await ReplyAsync($"Removed poll {pollName}");
            }
            else
            {
                await ReplyAsync($"Couldn't find an active poll with name {pollName}");
            }
        }

    }
}
