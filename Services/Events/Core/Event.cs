namespace Burgerama.Services.Events.Core
{
    public sealed class Event
    {
        public string Title { get; private set; }

        public Event(string title)
        {
            Title = title;
        }
    }
}
