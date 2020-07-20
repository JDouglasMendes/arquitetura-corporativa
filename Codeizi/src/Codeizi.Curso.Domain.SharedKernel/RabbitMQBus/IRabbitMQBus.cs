using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Domain.SharedKernel.RabbitMQBus
{
    public interface IRabbitMQBus
    {
        Task Publisher<T>(T publishable)
            where T : Publishable;
    }
}