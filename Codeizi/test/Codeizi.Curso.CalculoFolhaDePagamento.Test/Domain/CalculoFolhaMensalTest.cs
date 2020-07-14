using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class CalculoFolhaMensalTest
    {
        [Fact]
        public void CaculeFolhaMensal()
        {
            var contrato = CenarioContrato.CrieCenarioConsistente(1000);
            var calculo = new CalculoFolhaMensal();
            var result = calculo.Calcule(contrato);
            Assert.True(result.ExisteValores);
            new ValorComponenteCalculo(925).Should().Be(result.Valor(EnumComponentesCalculo.SalarioLiquido));
        }

        [Fact]
        public void BenchmarkCalculoMensal()
            => this.ExecutionTimeOf(x => x.CalculeBenchmark())
                .Should()
                .BeLessOrEqualTo(1000.Milliseconds());
                 
        private void CalculeBenchmark()
        {
            var contrato = CenarioContrato.CrieCenarioConsistente(1000);
            var calculo = new CalculoFolhaMensal();
            var execucoes = 5_00_000;
            while (execucoes > 0)
            {
                calculo.Calcule(contrato);
                execucoes--;
            }
            
        }
    }
}
