using Codeizi.Curso.Domain.Colaboradores;
using Codeizi.Curso.Domain.Colaboradores.CommandHandlers;
using Codeizi.Curso.Domain.Colaboradores.Commands;
using Codeizi.Curso.Domain.Colaboradores.Contracts;
using Codeizi.Curso.Domain.Contracts.Repository;
using Codeizi.Curso.Domain.SharedKernel.IMediatorBus;
using Codeizi.Curso.Domain.SharedKernel.Notifications;
using MediatR;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Curso.Domain.Test.Colaboradores
{
    public class ColaboradorCommandHandlerTest
    {
        [Fact]
        public async System.Threading.Tasks.Task AdmissaoConsistente()
        {
            var mockColaboradorRepository = Substitute.For<IColaboradorRepository>();
            await mockColaboradorRepository.RealizeAdmissao(Arg.Any<Colaborador>());

            var mockUOW = Substitute.For<IUnitOfWork>();
            mockUOW.Commit().Returns(true);

            var mockBus = Substitute.For<IMediatorHandler>();

            var mockNotificationHandler = Substitute.For<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            var admissaoColaborador = new AdmissaoColaboradorCommand("João", "Silva", new DateTime(2020, 1, 2), 10000, DateTime.Now.AddYears(-45));

            var commandAdmissao = new ColaboradorCommandHandler(mockColaboradorRepository,
                                                                mockUOW,
                                                                mockBus,
                                                                mockNotificationHandler);

            var result = await commandAdmissao.Handle(admissaoColaborador, CancellationToken.None);
            Assert.True(result);
        }

        [Fact]
        public async Task AdmissaoDadosColoboradorInconsistente()
        {
            var mockColaboradorRepository = Substitute.For<IColaboradorRepository>();
            await mockColaboradorRepository.RealizeAdmissao(Arg.Any<Colaborador>());

            var mockUOW = Substitute.For<IUnitOfWork>();
            mockUOW.Commit().Returns(true);

            var mockBus = Substitute.For<IMediatorHandler>();

            var mockNotificationHandler = Substitute.For<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            var admissaoColaborador = new AdmissaoColaboradorCommand(string.Empty, "Silva", new DateTime(2020, 1, 2), 10000, DateTime.Now.AddYears(-50));

            var commandAdmissao = new ColaboradorCommandHandler(mockColaboradorRepository,
                                                                mockUOW,
                                                                mockBus,
                                                                mockNotificationHandler);

            var result = await commandAdmissao.Handle(admissaoColaborador, CancellationToken.None);
            Assert.False(result);
        }
    }
}