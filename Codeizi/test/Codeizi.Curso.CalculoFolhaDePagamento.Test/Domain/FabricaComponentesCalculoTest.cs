using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class FabricaComponentesCalculoTest
    {
        [Theory]
        [InlineData(typeof(INSSComponenteDeCalculo))]
        [InlineData(typeof(IRRFComponenteDeCalculo))]
        [InlineData(typeof(FGTSComponenteCalculo))]
        [InlineData(typeof(SalarioLiquidoComponenteDeCalculo))]
        public void CriacaoObjetosTest(Type type)
            => Assert.IsType(type, FabricaComponentesCalculo.Singleton.Crie(type));
    }
}
