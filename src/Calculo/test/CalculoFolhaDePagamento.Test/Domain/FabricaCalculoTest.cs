using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using System;
using Xunit;

namespace CalculoFolhaDePagamento.Test.Domain
{
    public class FabricaCalculoTest
    {
        [Fact]
        public void Pegar_instancia_valida()
        {
            Assert.NotNull(FabricaCalculo.Crie(EnumFolhaDePagamento.Mensal, DateTime.Now));
            Assert.IsType<CalculoFolhaMensal>(FabricaCalculo.Crie(EnumFolhaDePagamento.Mensal, DateTime.Now));
            Assert.NotNull(FabricaCalculo.Crie(EnumFolhaDePagamento.Ferias, DateTime.Now));
            Assert.IsType<CalculoFolhaFerias>(FabricaCalculo.Crie(EnumFolhaDePagamento.Ferias, DateTime.Now));
            Assert.Throws<ArgumentException>(() => FabricaCalculo.Crie((EnumFolhaDePagamento)3, DateTime.Now));
        }
    }
}