using System.Threading.Tasks;

namespace Infra.CrossCutting.EventBusRabbitMQ
{
    public interface IConsumerServiceBus
    {
        Task Handle(Publishable publishable);

        string RoutingKey { get; }
    }
}