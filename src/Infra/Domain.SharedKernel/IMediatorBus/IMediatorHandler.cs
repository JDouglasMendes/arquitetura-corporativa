using Domain.SharedKernel.Commands;
using Domain.SharedKernel.Events;
using System.Threading.Tasks;

namespace Domain.SharedKernel.IMediatorBus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command)
            where T : Command;

        Task RaiseEvent<T>(T @event)
            where T : Event;
    }
}