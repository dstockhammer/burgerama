using System;
using System.Collections.Generic;
using Burgerama.Services.Voting.Core.DI;
using Microsoft.Practices.Unity;

namespace Burgerama.Services.Voting.DependencyResolution
{
    public class UnityServiceLocator : IServiceLocator
    {
        private readonly IUnityContainer _container;

        public UnityServiceLocator(IUnityContainer container)
        {
            _container = container;
        }

        public T GetInstance<T>()
        {
            try
            {
                return _container.Resolve<T>();
            }
            catch (ResolutionFailedException)
            {
                return default(T);
            }
        }

        public object GetInstance(Type type)
        {
            try
            {
                return _container.Resolve(type);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetAll(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _container.ResolveAll<T>();
        }
    }
}
