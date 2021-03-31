using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using FluentAssertions;
using FluentAssertions.Extensions;
using System;
using Xunit;

namespace CalculoFolhaDePagamento.Test.Domain
{
    public class CalculoFolhaMensalTest
    {
        [Fact]
        public void CaculeFolhaMensal()
        {
            var contrato = CenarioContrato.CrieCenarioConsistente(1000);
            var calculo = new CalculoFolhaMensal(DateTime.Now);
            var result = calculo.Calcule(contrato);
            Assert.True(result.ExisteValores);
            new ValorComponenteCalculo(925).Should().Be(result.Valor(EnumComponentesCalculo.SalarioLiquido));
        }

        [Fact]
        public void BenchmarkCalculoMensal()
            => this.ExecutionTimeOf(x => x.CalculeBenchmark())
                .Should()
                .BeLessOrEqualTo(5000.Milliseconds());

        private void CalculeBenchmark()
        {
            var contrato = CenarioContrato.CrieCenarioConsistente(1000);
            var calculo = new CalculoFolhaMensal(DateTime.Now);
            var execucoes = 5_00_000;
            while (execucoes > 0)
            {
                calculo.Calcule(contrato);
                execucoes--;
            }
        }
    }
}