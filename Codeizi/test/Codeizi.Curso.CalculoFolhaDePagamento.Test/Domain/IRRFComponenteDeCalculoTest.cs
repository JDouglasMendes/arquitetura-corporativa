using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
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
            var componente = new IRRFComponenteDeCalculo();
            var contrato = CenarioContrato.CrieCenarioConsistente(salario);
            var valorCalculado = componente.Calcule(contrato, new ComponentesCalculados(contrato));
            Assert.Equal((ValorComponenteCalculo)result, valorCalculado);
        }
    }
}
