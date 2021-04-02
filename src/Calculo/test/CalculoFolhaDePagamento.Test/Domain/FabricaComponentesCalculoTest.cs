using CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using System;
using Xunit;

namespace CalculoFolhaDePagamento.Test.Domain
{
    public class FabricaComponentesCalculoTest
    {
        [Theory]
        [InlineData(typeof(INSSComponenteDeCalculo))]
        [InlineData(typeof(IRRFComponenteDeCalculo))]
        [InlineData(typeof(FGTSComponenteCalculo))]
        [InlineData(typeof(SalarioLiquidoComponenteDeCalculo))]
        public void CriacaoObjetosTest(Type type)
            => Assert.IsType(type, FabricaComponentesCalculo.Crie(type));

        [Theory]
        [InlineData(typeof(FabricaComponentesCalculoTest))]
        public void CriacaoObjetosFalha(Type type)
            => Assert.Throws<ArgumentException>(() => FabricaComponentesCalculo.Crie(type));
    }
}