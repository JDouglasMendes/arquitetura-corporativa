using Codeizi.Curso.RH.Domain.Colaboradores.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Domain.Colaboradores.EventHandlers
{
    public class ColaboradorEventHandler :
       IRequestHandler<ColaboradorAdmitidoEvent, bool>
    {
        public Task<bool> Handle(ColaboradorAdmitidoEvent request, CancellationToken cancellationToken)
        {
            _ = request.SalarioContratual;
            _ = request.Nome;
            _ = request.Id;
            _ = request.MessageType;
            _ = request.ObservacaoContratual;
            _ = request.Sobrenome;
            _ = request.Timestamp;
            _ = request.AggregateId;
            _ = request.DataDeAdmissao;
            return Task.FromResult(true);
        }
    }
}