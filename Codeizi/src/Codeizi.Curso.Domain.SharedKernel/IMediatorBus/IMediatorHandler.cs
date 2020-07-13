using Codeizi.Curso.Domain.SharedKernel.Commands;
using Codeizi.Curso.Domain.SharedKernel.Events;
using System.Threading.Tasks;

namespace Codeizi.Curso.Domain.SharedKernel.IMediatorBus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command)
            where T : Command;
        Task RaiseEvent<T>(T @event)
            where T : Event;
    }
}
