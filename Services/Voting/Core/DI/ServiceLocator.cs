using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Services.Voting.Core.DI
{
    public static class ServiceLocator
    {
        public static IServiceLocator Current { get; private set; }

        public static void SetServiceLocator(Func<IServiceLocator> create)
        {
            Contract.Requires<ArgumentNullException>(create != null);

            Current = create();
        }
    }
}
