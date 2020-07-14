using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class SalarioLiquidoComponenteDeCalculoTest
    {
        [Theory]
        [InlineData(1000, 20, 40, 940)]
        public void Calcule(double salarioContratual,
                            double inss,
                            double irrf,
                            double result)
        {
            var tabela = new ComponentesCalculados(null);
            tabela.AdicioneValor(new INSSComponenteDeCalculo(), (ValorComponenteCalculo)inss);
            tabela.AdicioneValor(new IRRFComponenteDeCalculo(), (ValorComponenteCalculo)irrf);
            var contrato = CenarioContrato.CrieCenarioConsistente(salarioContratual);
            var compomente = new SalarioLiquidoComponenteDeCalculo();
            var valorCalculado = compomente.Calcule(contrato, tabela);
            Assert.Equal(result, valorCalculado.Valor);

        }

    }
}
