using Codeizi.Curso.Domain.Colaboradores.Commands;
using Codeizi.Curso.Domain.Colaboradores.Contracts;
using Codeizi.Curso.Domain.Colaboradores.Events;
using Codeizi.Curso.Domain.CommandHandlers;
using Codeizi.Curso.Domain.Contracts.Repository;
using Codeizi.Curso.Domain.SharedKernel.IMediatorBus;
using Codeizi.Curso.Domain.SharedKernel.Notifications;
using Codeizi.Curso.Domain.SharedKernel.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.Domain.Colaboradores.CommandHandlers
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
            var colaborador = new Colaborador(Guid.NewGuid(), NomePessoa.Crie(request.Nome, request.Sobrenome))
            {
                ObservacaoContratual = request.ObservacaoContratual,
            };
            colaborador.AddContrato(request.DataDeAdmissao, request.SalarioContratual);
            await _colaboradorRepository.RealizeAdmissao(colaborador);

            var colaboradorAdmitidoEvent = new ColaboradorAdmitidoEvent(colaborador.Id,
                                                                        colaborador.Nome.Nome,
                                                                        colaborador.Nome.Sobrenome,
                                                                        request.DataDeAdmissao,
                                                                        request.SalarioContratual,
                                                                        colaborador.ObservacaoContratual);
            if (Commit())
                await Bus.RaiseEvent(colaboradorAdmitidoEvent);

            return true;
        }
    }
}
