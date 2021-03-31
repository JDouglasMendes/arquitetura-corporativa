using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Services.Repositories;
using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using Infra.CrossCutting.EventBusRabbitMQ;
using Infra.CrossCutting.Identity;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CalculoFolhaDePagamento.Test.Domain
{
    public class CalculoBuilderTest
    {
        [Fact]
        public void CalculeContratos()
        {
            var quantidadeCalls = 500;
            var mockCalculoRepository = Substitute.For<ICalculoRepository>();
            mockCalculoRepository
                .InsiraValoresCalculados(Arg.Any<ComponentesCalculados>())
                .Returns(Task.CompletedTask);

            var mockContratoRepository = Substitute.For<IContratoRepository>();
            mockContratoRepository
                .ObterContratosVigentes(Arg.Any<DateTime>())
                .Returns(CenarioContrato.CrieCenarioValido(quantidadeCalls));

            var builder = new CalculoBuilder(DateTime.Now,
                                             EnumFolhaDePagamento.Mensal,
                                             mockCalculoRepository,
                                             Substitute.For<IFeedbackExecucaoCalculoServiceDomain>());

            var task = builder.InicieCalculo(mockContratoRepository)
                                .CalculeContratos();

            while (task.Status != TaskStatus.RanToCompletion) { }
            Assert.True(builder.IdExecucao != Guid.Empty);
            mockCalculoRepository.Received(quantidadeCalls).InsiraValoresCalculados(Arg.Any<ComponentesCalculados>());
        }

        [Fact]
        public async void CalculeContratosChamandoInicioDeFormaIndevida()
        {
            var quantidadeCalls = 500;
            var mockCalculoRepository = Substitute.For<ICalculoRepository>();
            mockCalculoRepository
                .InsiraValoresCalculados(Arg.Any<ComponentesCalculados>())
                .Returns(Task.CompletedTask);

            var mockContratoRepository = Substitute.For<IContratoRepository>();
            mockContratoRepository
                .ObterContratosVigentes(Arg.Any<DateTime>())
                .Returns(CenarioContrato.CrieCenarioValido(quantidadeCalls));

            var mockRabbtimq = Substitute.For<IRabbitMQBus>();

            var mockUser = Substitute.For<IUser>();

            var builder = new CalculoBuilder(DateTime.Now,
                                             EnumFolhaDePagamento.Mensal,
                                             mockCalculoRepository,
                                             Substitute.For<IFeedbackExecucaoCalculoServiceDomain>());

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => builder.CalculeContratos());
        }
    }
}