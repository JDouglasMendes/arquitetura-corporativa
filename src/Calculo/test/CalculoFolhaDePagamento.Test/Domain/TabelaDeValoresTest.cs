using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using System;
using Xunit;

namespace CalculoFolhaDePagamento.Test.Domain
{
    public class TabelaDeValoresTest
    {
        [Theory]
        [InlineData(10, true)]
        [InlineData(0, false)]
        public void AdicioneValor(double valor, bool result)
        {
            var contrato = CenarioContrato.CrieCenarioConsistente(valor);
            var inss = new INSSComponenteDeCalculo();
            var tabela = new ComponentesCalculados(contrato, new DateTime(2020,1,1));
            tabela.AdicioneValor(inss, (ValorComponenteCalculo)valor);
            Assert.True(tabela.ExisteValores == result);
            Assert.Equal(new DateTime(2020, 1, 1), tabela.Referencia);
            Assert.Equal(contrato.IdColaborador, tabela.IdColaborador);
            Assert.Equal(contrato.IdContrato, tabela.IdContrato);
            Assert.True(tabela.Valores.Count > 0 == result);
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