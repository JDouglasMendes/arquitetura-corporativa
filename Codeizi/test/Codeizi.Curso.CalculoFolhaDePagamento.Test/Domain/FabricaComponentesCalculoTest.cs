using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using System;
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