using Infra.CrossCutting.EventBusRabbitMQ;
using RabbitMQ.Client;

namespace RH.Service.Api.IntegrationFake
{
#pragma warning disable CA1063 // Implement IDisposable Correctly

    public class RabbitMQPersistentConnectionFake : IRabbitMQPersistentConnection
#pragma warning restore CA1063 // Implement IDisposable Correctly
    {
        public bool IsConnected => true;

        public IModel CreateModel()
            => null;

#pragma warning disable CA1063 // Implement IDisposable Correctly

        public void Dispose()
#pragma warning restore CA1063 // Implement IDisposable Correctly
        { }

        public bool TryConnect()
            => true;
    }
}