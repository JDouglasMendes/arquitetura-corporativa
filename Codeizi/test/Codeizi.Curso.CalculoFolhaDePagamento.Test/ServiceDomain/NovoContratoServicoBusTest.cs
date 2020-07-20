using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.BusModel;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.ServiceDomain
{
    public class NovoContratoServicoBusTest
    {
        [Fact]
        public void HandleSucesso()
        {
            var mock = Substitute.For<IContratoRepository>();
            mock.InsiraNovoContrato(Arg.Any<ContratoBusModel>(), 
                                    Arg.Any<Func<ContratoBusModel, Contrato>>());
            var bus = new NovoContratoServicoBus(mock);
            var result = bus.Handle(CenarioContratoBusModel.CrieContrato.ToJson());
            result.ConfigureAwait(false);
            Assert.Null(result.Exception);
            Assert.Equal(TaskStatus.RanToCompletion, result.Status) ;
        }

        [Fact]
        public void ConvertToTest()
        {
            var busModel = CenarioContratoBusModel.CrieContrato;
            var mock = Substitute.For<IContratoRepository>();
            var bus = new NovoContratoServicoBus(mock);
            var contrato = bus.ConvertTo(busModel);
            contrato.Vigencia.Fim.Should().Be(busModel.DataFim);
            contrato.Vigencia.Inicio.Should().Be(busModel.DataInicio);
            contrato.IdColaborador.Should().Be(busModel.IdColaborador);
            contrato.IdContrato.Should().Be(busModel.IdContrato);
            contrato.SalarioContratual.Valor.Should().Be(busModel.SalarioContratual);
        }
    }
}