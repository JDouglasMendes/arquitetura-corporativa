using System.Threading.Tasks;

namespace Infra.CrossCutting.EventBusRabbitMQ
{
    public interface IRabbitMQBus
    {
        Task Publisher<T>(T publishable)
            where T : Publishable;
    }
}