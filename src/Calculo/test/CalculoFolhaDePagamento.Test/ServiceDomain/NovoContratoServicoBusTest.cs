using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using CalculoFolhaDePagamento.Domain.Services.BusModel;
using CalculoFolhaDePagamento.Domain.Services.Repositories;
using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using FluentAssertions;
using Infra.CrossCutting.Configuration;
using Infra.CrossCutting.EventBusRabbitMQ;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CalculoFolhaDePagamento.Test.ServiceDomain
{
    public class NovoContratoServicoBusTest
    {
        [Fact]
        public void HandleSucesso()
        {
            var mock = Substitute.For<IContratoRepository>();
            mock.InsiraNovoContrato(Arg.Any<ContratoBusModel>(),
                                    Arg.Any<Func<ContratoBusModel, Contrato>>());
            var mockConfiguration = Substitute.For<ICodeiziConfiguration>();

            var bus = new NovoContratoServicoBus(mock, mockConfiguration);
            var contrato = CenarioContratoBusModel.CrieContrato;
            var publishable = FactoryPublishable.Get(contrato.IdColaborador, "add-contrato", contrato);
            var result = bus.Handle(publishable);
            result.ConfigureAwait(false);
            Assert.Null(result.Exception);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status);
        }

        [Fact]
        public void ConvertToTest()
        {
            var busModel = CenarioContratoBusModel.CrieContrato;
            var contrato = NovoContratoServicoBus.ConvertTo(busModel);
            contrato.Vigencia.Fim.Should().Be(busModel.DataFim);
            contrato.Vigencia.Inicio.Should().Be(busModel.DataInicio);
            contrato.IdColaborador.Should().Be(busModel.IdColaborador);
            contrato.IdContrato.Should().Be(busModel.IdContrato);
            contrato.SalarioContratual.Valor.Should().Be(busModel.SalarioContratual);
        }
    }
}