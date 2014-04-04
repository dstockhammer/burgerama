namespace Burgerama.Services.Outings.Core
{
    public sealed class Outing
    {
        public string Title { get; private set; }

        public Outing(string title)
        {
            Title = title;
        }
    }
}
