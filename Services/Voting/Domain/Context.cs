using System.Collections.Generic;

namespace Burgerama.Services.Voting.Domain
{
    public sealed class Context
    {
        public string Key { get; private set; }

        public IEnumerable<Candidate> Campaigns { get; private set; }

        public Context(string key, IEnumerable<Candidate> campaigns)
        {
            Key = key;
            Campaigns = campaigns;
        }
    }
}
