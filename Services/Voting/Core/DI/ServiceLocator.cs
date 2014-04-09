using System;

namespace Burgerama.Services.Voting.Core.DI
{
    public static class ServiceLocator
    {
        public static IServiceLocator Current { get; private set; }

        public static void SetServiceLocator(Func<IServiceLocator> create)
        {
            Current = create();
        }
    }
}
