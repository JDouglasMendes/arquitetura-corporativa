using Codeizi.Curso.RH.Domain.Colaboradores.Contracts;
using Codeizi.Curso.RH.Domain.CommandHandlers;
using Codeizi.Curso.RH.Domain.Contracts.Repository;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Events;
using Codeizi.Curso.RH.Domain.SharedKernel.IMediatorBus;
using Codeizi.Curso.RH.Domain.SharedKernel.Notifications;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.CommandHandlers
{
    public class FeriasCommandHandler : CommandHandler,
          IRequestHandler<RegistrarOcorrenciaDeFeriasCommand, bool>
    {
        private readonly IOcorrenciaDeDeriasRepository _ocorrenciaDeDeriasRepository;
        private readonly IColaboradorRepository _colaboradorRepository;

        public FeriasCommandHandler(IOcorrenciaDeDeriasRepository ocorrenciaDeDeriasRepository,
                                   IColaboradorRepository colaboradorRepository,
                                   IUnitOfWork uow,
                                   IMediatorHandler bus,
                                   INotificationHandler<DomainNotification> notifications)
        : base(uow, bus, notifications)

        {
            _ocorrenciaDeDeriasRepository = ocorrenciaDeDeriasRepository;
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task<bool> Handle(RegistrarOcorrenciaDeFeriasCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var colaborador = await _colaboradorRepository.BusqueColaborador(request.IdColaborador);

            var contrato = colaborador.Contratos.FirstOrDefault(x => x.Id == request.IdContrato);

            if (contrato == null)
                contrato = await _colaboradorRepository.ObtenhaContrato(request.IdContrato);

            var ocorrenciaDeFerias = new OcorrenciaDeFerias(contrato, request.DataDeInicio, request.DiasDeFerias, request.DiasDeAbono);

            await _ocorrenciaDeDeriasRepository.RegistrarOcorrenciaDeFeriasCommand(ocorrenciaDeFerias);

            if (Commit())
            {
                await Bus.RaiseEvent(AgendamentoDeFeriasQueryEvent.Crie(colaborador, request.IdContrato, ocorrenciaDeFerias));


                return true;
            }

            return false;
        }
    }
}