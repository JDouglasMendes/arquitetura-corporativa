using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using System;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class FGTSComponenteCalculoTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 8)]
        [InlineData(1000, 80)]
        [InlineData(10000, 800)]
        public void Calcule(double salario, double result)
        {
            var contrato = CenarioContrato.CrieCenarioConsistente(salario);
            var tabela = new ComponentesCalculados(contrato, DateTime.Now);
            tabela.AdicioneValor(new BaseSalarioComponenteCalculo(), (ValorComponenteCalculo)salario);
            var componente = new FGTSComponenteCalculo();
            var valorCalculado = componente.Calcule(contrato, tabela);
            Assert.Equal((ValorComponenteCalculo)result, valorCalculado);
        }
    }
}