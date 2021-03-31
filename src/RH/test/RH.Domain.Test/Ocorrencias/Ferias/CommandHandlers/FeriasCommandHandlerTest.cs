using Domain.SharedKernel.IMediatorBus;
using Domain.SharedKernel.Notifications;
using Domain.SharedKernel.ValueObjects;
using MediatR;
using NSubstitute;
using RH.Domain.Colaboradores;
using RH.Domain.Colaboradores.Contracts;
using RH.Domain.Contracts.Repository;
using RH.Domain.Ocorrencias.Ferias;
using RH.Domain.Ocorrencias.Ferias.CommandHandlers;
using RH.Domain.Ocorrencias.Ferias.Contracts;
using RH.Domain.Ocorrencias.Ferias.Validations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Test.Ocorrencias.Ferias.CommandHandlers
{
    [Trait("Categoria", "Domain")]
    public class FeriasCommandHandlerTest
    {
        [Fact]
        public async Task Registrar_ocorrencia_de_ferias_com_sucesso()
        {
            // Arrange
            var mockContrato = Substitute.For<Contrato>();
            var mockOcorrenciaRepository = Substitute.For<IOcorrenciaDeDeriasRepository>();
            var validation = new RegistrarOcorrenciaDeFeriasCommandValidation(mockOcorrenciaRepository);
            mockOcorrenciaRepository.ObtenhaOcorrenciasDoPeriodoAquisitivo(Arg.Any<Guid>(), Arg.Any<DateTime>())
                .Returns(new List<OcorrenciaDeFerias>());
            var mockColaboradorRepository = Substitute.For<IColaboradorRepository>();

            mockColaboradorRepository.BusqueColaborador(Arg.Any<Guid>())
                .Returns(new Colaborador(Guid.NewGuid(), NomePessoa.Crie("Codeizi", "Treinamento"), DateTime.Now.AddYears(-33)));
            mockColaboradorRepository.ObtenhaContrato(Arg.Any<Guid>()).Returns(mockContrato);
            var mockUOW = Substitute.For<IUnitOfWork>();
            mockUOW.Commit().ReturnsForAnyArgs(true);

            var mockMediator = Substitute.For<IMediatorHandler>();
            var mockNotification = Substitute.For<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            var feriasCommand = new FeriasCommandHandler(mockOcorrenciaRepository,
                mockColaboradorRepository,
                mockUOW,
                mockMediator,
                mockNotification);

            var request = CenarioOcorrenciaDeFeriasCommandBuilder.Crie(validation);

            // Act
            var result = await feriasCommand.Handle(request, CancellationToken.None);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Registrar_ocorrencia_de_ferias_com_falha_de_validacao()
        {
            // Arrange
            var mockContrato = Substitute.For<Contrato>();
            var mockOcorrenciaRepository = Substitute.For<IOcorrenciaDeDeriasRepository>();
            var validation = new RegistrarOcorrenciaDeFeriasCommandValidation(mockOcorrenciaRepository);
            mockOcorrenciaRepository.ObtenhaOcorrenciasDoPeriodoAquisitivo(Arg.Any<Guid>(), Arg.Any<DateTime>())
                .Returns(new List<OcorrenciaDeFerias>() {
                    new OcorrenciaDeFerias(mockContrato, new DateTime(2020, 1, 2), 30, 0),
                });
            var mockColaboradorRepository = Substitute.For<IColaboradorRepository>();
            mockColaboradorRepository.ObtenhaContrato(Arg.Any<Guid>()).Returns(mockContrato);
            var mockUOW = Substitute.For<IUnitOfWork>();
            mockUOW.Commit().ReturnsForAnyArgs(true);

            var mockMediator = Substitute.For<IMediatorHandler>();
            var mockNotification = Substitute.For<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            var feriasCommand = new FeriasCommandHandler(mockOcorrenciaRepository,
                mockColaboradorRepository,
                mockUOW,
                mockMediator,
                mockNotification);

            var request = CenarioOcorrenciaDeFeriasCommandBuilder.Crie(validation);

            // Act
            var result = await feriasCommand.Handle(request, CancellationToken.None);
            // Assert
            Assert.False(result);
        }
    }
}