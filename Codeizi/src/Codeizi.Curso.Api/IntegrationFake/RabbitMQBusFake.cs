using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Api.IntegrationFake
{
    public class RabbitMQBusFake : IRabbitMQBus
    {
        public Task Publisher<T>(T publishable)
            where T : Publishable
            => Task.CompletedTask;
    }
}