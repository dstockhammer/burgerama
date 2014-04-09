using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Core.DI
{
    public interface IServiceLocator
    {
        T GetInstance<T>();

        object GetInstance(Type type);

        IEnumerable<object> GetAll(Type serviceType);

        IEnumerable<T> GetAll<T>();
    }
}
