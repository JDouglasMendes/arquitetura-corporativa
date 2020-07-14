using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class FGTSComponenteCalculoTest
    {
        [Theory]
        [InlineData(0,0)]
        [InlineData(100, 8)]
        [InlineData(1000, 80)]
        [InlineData(10000, 800)]
        public void Calcule(double salario, double result)
        {            
            var componente = new FGTSComponenteCalculo();
            var contrato = CenarioContrato.CrieCenarioConsistente(salario);
            var valorCalculado = componente.Calcule(contrato, new ComponentesCalculados(contrato));
            Assert.Equal((ValorComponenteCalculo)result, valorCalculado);
        }
    }
}
