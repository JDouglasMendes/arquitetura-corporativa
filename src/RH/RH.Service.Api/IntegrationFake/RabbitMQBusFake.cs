using Infra.CrossCutting.EventBusRabbitMQ;
using System.Threading.Tasks;

namespace RH.Service.Api.IntegrationFake
{
    public class RabbitMQBusFake : IRabbitMQBus
    {
        public Task Publisher<T>(T publishable)
            where T : Publishable
            => Task.CompletedTask;
    }
}