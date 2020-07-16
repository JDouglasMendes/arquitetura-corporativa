using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using System;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class ContratoTest
    {
        [Fact]
        public void ContratoPadraoTest()
        {
            var contrato = CenarioContrato.CrieCenarioValido();
            Assert.True(Guid.TryParse(contrato.IdColaborador.ToString(), out _));
            Assert.True(contrato.SalarioContratual > ValorComponenteCalculo.Zero);
            Assert.True(contrato.Vigencia.Inicio != default);
            Assert.True(contrato.Vigencia.Fim == default);
            Assert.True(contrato.Ativo);
        }

        [Fact]
        public void ContratoFinalizadoTest()
        {
            var contrato = CenarioContrato.CrieCenarioValido();
            contrato.FinalizeContrato(DateTime.Now);
            Assert.False(contrato.Ativo);
        }
    }
}