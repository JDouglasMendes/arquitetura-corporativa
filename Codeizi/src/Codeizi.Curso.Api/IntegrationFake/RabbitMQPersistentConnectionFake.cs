using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Api.IntegrationFake
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
