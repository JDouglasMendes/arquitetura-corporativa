using Infra.CrossCutting.EventBusRabbitMQ;
using RabbitMQ.Client;

namespace RH.Service.Api.IntegrationFake
{
    public class RabbitMQPersistentConnectionFake : IRabbitMQPersistentConnection

    {
        private bool disposedValue;

        public bool IsConnected => true;

        public IModel CreateModel()
            => null;

        public bool TryConnect()
            => true;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}