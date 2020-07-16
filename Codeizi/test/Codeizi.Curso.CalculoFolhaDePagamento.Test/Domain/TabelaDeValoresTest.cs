using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using System;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class TabelaDeValoresTest
    {
        [Theory]
        [InlineData(10, true)]
        [InlineData(0, false)]
        public void AdicioneValor(double valor, bool result)
        {
            var inss = new INSSComponenteDeCalculo();
            var tabela = new ComponentesCalculados(CenarioContrato.CrieCenarioConsistente(valor), DateTime.Now);
            tabela.AdicioneValor(inss, (ValorComponenteCalculo)valor);
            Assert.True(tabela.ExisteValores == result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        public void ObterValorTest(double valor)
        {
            var inss = new INSSComponenteDeCalculo();
            var tabela = new ComponentesCalculados(CenarioContrato.CrieCenarioConsistente(valor), DateTime.Now);
            tabela.AdicioneValor(inss, (ValorComponenteCalculo)valor);
            tabela.AdicioneValor(new FGTSComponenteCalculo(), (ValorComponenteCalculo)valor);
            Assert.Equal((ValorComponenteCalculo)valor, tabela.Valor(EnumComponentesCalculo.FGTS));
        }
    }
}