using System.Threading.Tasks;

namespace Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ
{
    public interface IRabbitMQBus
    {
        Task Publisher<T>(T publishable)
            where T : Publishable;
    }
}