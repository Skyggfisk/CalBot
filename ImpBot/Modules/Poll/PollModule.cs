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
        private static readonly List<PollModel> PollList = new List<PollModel>();

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

            PollList.Add(poll);

            var stringBuilder = new StringBuilder();
            foreach (var x in pollItems)
            {
                stringBuilder.AppendLine(x.Trim());
            }

            await ReplyAsync($"created poll named \"{poll.PollName.ToUpper()}\" with options: \n{stringBuilder}");
        }

        // TODO: Stuff like naming and other fiddly parameters
        // cast a vote for an item in an active poll
        // Example: !imp vote ChangeGuildName? yes
        [Command("Vote")]
        public async Task Vote(string pollName, string itemName)
        {
            var user = Context.Message.Author;
            var poll = PollList.Find(x => x.PollName == pollName);

            var itemToVoteFor = poll.PollItems.Find(y => y.ItemName == itemName);

            if (!poll.VotedUsersList.Contains(user))
            {
                poll.VotedUsersList.Add(user);
                itemToVoteFor.ItemVotes++;
                await ReplyAsync($"{user} voted for {itemName}");
            }
            else
            {
                await ReplyAsync("You've already voted.");
            }


        }


        // TODO: End the poll and broadcast the results to the guild channel.
        [Command("EndPoll")]
        public async Task EndPoll(string pollName)
        {
            var user = Context.Message.Author;

            // Not the right way to check. But nested = nasty :(
            // Edit: 2 mins later... solution?
            if (PollList.Any(x => x.PollName == pollName && x.Creator == user))
            {
                var poll = PollList.Find(x => x.PollName == pollName);
                var stringBuilder = new StringBuilder();

                foreach (var x in poll.PollItems)
                {
                    stringBuilder.AppendLine($"{x.ItemName}: {x.ItemVotes}");
                }
                await ReplyAsync($"Concluded poll \"{pollName.ToUpper()}\" with results: \n{stringBuilder}");
            }
            else
            {
                await ReplyAsync("Couldn't find poll.");
            }

        }

        // TODO: Needs to check both ifExists and isActive.
        // Ends a poll prematurely without broadcasting results.
        [Command("CancelPoll")]
        public async Task CancelPoll(string pollName)
        {
            var user = Context.Message.Author;

            if (PollList.Any(x => x.PollName == pollName) && PollList.Any(x => x.Creator == user))
            {
                PollList.RemoveAll(x => x.PollName == pollName); // bug: anybody can remove anything. Check EndPoll().
                await ReplyAsync($"Removed poll \"{pollName.ToUpper()}\"");
            }
            else
            {
                await ReplyAsync($"Couldn't find an active poll with name \"{pollName.ToUpper()}\" or caller is not poll creator.");
            }
        }

    }
}
