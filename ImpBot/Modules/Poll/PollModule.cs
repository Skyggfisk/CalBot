using System.Collections.Generic;
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

        [Command("PollTest")]
        public async Task PollTest()
        {
            await ReplyAsync("test from poll module");
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

            //await ReplyAsync("");
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

            await ReplyAsync("NYI");
        }

    }
}
