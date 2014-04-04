namespace Burgerama.Services.Venues.Core
{
    public sealed class Venue
    {
        public string Title { get; private set; }

        public Venue(string title)
        {
            Title = title;
        }
    }
}
