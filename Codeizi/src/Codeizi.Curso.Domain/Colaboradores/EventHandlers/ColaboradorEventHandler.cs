using Codeizi.Curso.Domain.Colaboradores.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.Domain.Colaboradores.EventHandlers
{
    public class ColaboradorEventHandler :
        INotificationHandler<ColaboradorAdmitidoEvent>
    {
        public Task Handle(ColaboradorAdmitidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}