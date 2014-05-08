namespace Burgerama.Messaging.MassTransit.Endpoint.Topshelf
{
    public interface IEndpointService
    {
        void Start();

        void Stop();
    }
}