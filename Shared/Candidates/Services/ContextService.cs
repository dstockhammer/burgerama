using Burgerama.Shared.Candidates.Domain;
using Burgerama.Shared.Candidates.Domain.Contracts;
using Burgerama.Shared.Candidates.Services.Contracts;
using Serilog;

namespace Burgerama.Shared.Candidates.Services
{
    public sealed class ContextService : IContextService
    {
        private readonly ILogger _logger;
        private readonly IContextRepository _contextRepository;

        public ContextService(ILogger logger, IContextRepository contextRepository)
        {
            _logger = logger;
            _contextRepository = contextRepository;
        }

        public bool CreateContext(string contextKey, bool gracefullyHandleUnknownCandidates)
        {
            var context = _contextRepository.Get(contextKey);
            if (context != null)
            {
                _logger.Error("Tried to create context {ContextKey}, but it already exists.", contextKey);
                return false;
            }
            
            _contextRepository.SaveOrUpdate(new Context(contextKey, gracefullyHandleUnknownCandidates));
            _logger.Information("Created context {ContextKey}.", contextKey);

            return true;
        }
    }
}
