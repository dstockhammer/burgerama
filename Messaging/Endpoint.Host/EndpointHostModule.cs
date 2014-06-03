using Autofac;

namespace Burgerama.Messaging.Endpoint.Host
{
    public sealed class EndpointHostModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EndpointHostFactory>().AsSelf();
            builder.RegisterType<EndpointService>().As<IEndpointService>();
        }
    }
}
