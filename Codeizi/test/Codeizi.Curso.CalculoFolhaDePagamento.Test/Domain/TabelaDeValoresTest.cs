using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
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
            var tabela = new ComponentesCalculados(CenarioContrato.CrieCenarioConsistente(valor));
            tabela.AdicioneValor(inss, (ValorComponenteCalculo)valor);
            Assert.True(tabela.ExisteValores == result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        public void ObterValorTest(double valor)
        {
            var inss = new INSSComponenteDeCalculo();
            var tabela = new ComponentesCalculados(CenarioContrato.CrieCenarioConsistente(valor));
            tabela.AdicioneValor(inss, (ValorComponenteCalculo)valor);
            tabela.AdicioneValor(new FGTSComponenteCalculo(), (ValorComponenteCalculo)valor);
            Assert.Equal((ValorComponenteCalculo)valor, tabela.Valor(EnumComponentesCalculo.FGTS));
        }
    }
}
