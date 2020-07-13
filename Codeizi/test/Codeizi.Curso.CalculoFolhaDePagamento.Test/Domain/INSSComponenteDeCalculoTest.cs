using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class INSSComponenteDeCalculoTest
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(1045, 1045 * 7.5 / 100)]
        [InlineData(2089.60, 2089.60 * 9 / 100)]
        [InlineData(3131.40, 3131.40 * 12 / 100)]
        [InlineData(6101.06, 6101.06 * 14 / 100)]
        [InlineData(double.MaxValue, 854.1484)]
        public void Calcule(double salario, double result)
        {
            var componente = new INSSComponenteDeCalculo();
            var contrato = CenarioContrato.CrieCenarioConsistente(salario);
            var valorCalculado = componente.Calcule(contrato, new ComponentesCalculados());
            Assert.Equal((ValorComponenteCalculo)result, valorCalculado);
        }
    }
}
