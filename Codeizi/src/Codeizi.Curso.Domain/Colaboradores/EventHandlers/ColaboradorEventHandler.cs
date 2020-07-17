using Codeizi.Curso.Domain.Colaboradores.Commands;
using Codeizi.Curso.Domain.Colaboradores.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.Domain.Colaboradores.EventHandlers
{
    public class ColaboradorEventHandler :
       IRequestHandler<ColaboradorAdmitidoEvent, bool>
    {
        public Task<bool> Handle(ColaboradorAdmitidoEvent request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}