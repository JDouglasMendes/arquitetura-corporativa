using RabbitMQ.Client;
using System;

namespace Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}