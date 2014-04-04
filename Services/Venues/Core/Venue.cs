using System;

namespace Burgerama.Services.Venues.Core
{
    public sealed class Venue
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public int Votes
        {
            get { return 0; }
        }

        public double Rating
        {
            get { return 0; }
        }

        public Venue(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public Venue(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }

        public void AddVote(Guid user)
        {
            // todo: only allow to vote if there is no outing with this venue

            var vote = new Vote(user);

            // todo: do something to actually add the vote
        }

        public void AddRating(Guid user, int value)
        {
            // todo: only allow to rate if the user has attendend an outing with this venue

            var rating = new Rating(user, value);

            // todo: do something to actually add the rating
        }
    }
}
