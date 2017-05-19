using System.Collections.Generic;
using Discord;


namespace ImpBot.Modules.Poll
{
    public class PollModel
    {
        public string PollName { get; set; }
        public IUser Creator { get; set; }
        public bool IsActive { get; set; }
        public List<PollItem> PollItems { get; set; }
        public List<IUser> VotedUsersList { get; set; }
    }

    public class PollItem
    {
        public string ItemName { get; set; }
        public int ItemVotes { get; set; }
    }
}
