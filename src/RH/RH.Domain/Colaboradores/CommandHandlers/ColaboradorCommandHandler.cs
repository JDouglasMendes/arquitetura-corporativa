using Domain.SharedKernel.IMediatorBus;
using Domain.SharedKernel.Notifications;
using Domain.SharedKernel.ValueObjects;
using MediatR;
using RH.Domain.Colaboradores.Commands;
using RH.Domain.Colaboradores.Contracts;
using RH.Domain.Colaboradores.Events;
using RH.Domain.CommandHandlers;
using RH.Domain.Contracts.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Domain.Colaboradores.CommandHandlers
{
    public class ColaboradorCommandHandler : CommandHandler,
        IRequestHandler<AdmissaoColaboradorCommand, bool>
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorCommandHandler(IColaboradorRepository colaboradorRepository,
                                         IUnitOfWork uow,
                                         IMediatorHandler bus,
                                         INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
            => _colaboradorRepository = colaboradorRepository;

        public async Task<bool> Handle(AdmissaoColaboradorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var colaborador = new Colaborador(Guid.NewGuid(), NomePessoa.Crie(request.Nome, request.Sobrenome), request.DataNascimento)
            {
                ObservacaoContratual = request.ObservacaoContratual,
            };

            colaborador.AddContrato(request.DataDeAdmissao, request.SalarioContratual);
            await _colaboradorRepository.RealizeAdmissao(colaborador);

            if (Commit())
            {
                var contratoParaCalculo = Bus.RaiseEvent(NovoColaboradorParaCalculoEvent.Crie(colaborador));

                var eventSource = Bus.RaiseEvent(ColaboradorEventSource.Crie(colaborador));

                var contratoQuery = Bus.RaiseEvent(ContratoQueryEvent.Crie(colaborador));

                Task.WaitAll(contratoParaCalculo, eventSource, contratoQuery);

                return true;
            }

            return false;
        }
    }
}