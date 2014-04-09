using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Burgerama.Services.Voting.Core.DI
{
    public class HttpDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            return ServiceLocator.Current.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ServiceLocator.Current.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            // nothing to do
        }
    }
}
