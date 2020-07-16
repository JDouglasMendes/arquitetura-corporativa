using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using System;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class INSSComponenteDeCalculoTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1045, 1045 * 7.5 / 100)]
        [InlineData(2089.60, 2089.60 * 9 / 100)]
        [InlineData(3131.40, 3131.40 * 12 / 100)]
        [InlineData(6101.06, 6101.06 * 14 / 100)]
        [InlineData(double.MaxValue, 854.1484)]
        public void Calcule(double salario, double result)
        {
            var contrato = CenarioContrato.CrieCenarioConsistente(salario);
            var tabela = new ComponentesCalculados(contrato, DateTime.Now);
            tabela.AdicioneValor(new BaseSalarioComponenteCalculo(), (ValorComponenteCalculo)salario);
            var componente = new INSSComponenteDeCalculo();
            var valorCalculado = componente.Calcule(contrato, tabela);
            Assert.Equal((ValorComponenteCalculo)result, valorCalculado);
        }
    }
}