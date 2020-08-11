using System.Threading.Tasks;

namespace Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ
{
    public interface IConsumerServiceBus
    {
        Task Handle(Publishable publishable);

        string RoutingKey { get; }
    }
}