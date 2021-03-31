using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using System;
using Xunit;

namespace CalculoFolhaDePagamento.Test.Domain
{
    public class IRRFComponenteDeCalculoTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1903.98, 0)]
        [InlineData(2826.65, 2826.65 * 7.5 / 100)]
        [InlineData(3751.05, 3751.05 * 15 / 100)]
        [InlineData(4664.68, 4664.68 * 22.5 / 100)]
        [InlineData(4664.69, 4664.69 * 27.5 / 100)]
        [InlineData(double.MaxValue, double.MaxValue * 27.5 / 100)]
        public void Calcule(double salario, double result)
        {
            var contrato = CenarioContrato.CrieCenarioConsistente(salario);
            var tabela = new ComponentesCalculados(contrato, DateTime.Now);
            tabela.AdicioneValor(new BaseSalarioComponenteCalculo(), (ValorComponenteCalculo)salario);
            var componente = new IRRFComponenteDeCalculo();
            var valorCalculado = componente.Calcule(contrato, tabela);
            Assert.Equal((ValorComponenteCalculo)result, valorCalculado);
        }
    }
}