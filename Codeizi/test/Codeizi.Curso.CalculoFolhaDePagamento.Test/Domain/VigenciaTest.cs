using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;
using Xunit;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Test.Domain
{
    public class VigenciaTest
    {
        [Fact]
        public void IgualdadeVigencias()
        {
            var v1 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            var v2 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            Assert.True(v1.Equals(v2));
            Assert.True(v1 == v2);
            Assert.True(((object)v1).Equals(v2));
        }

        [Fact]
        public void DiferencaVigencias()
        {
            var v1 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            var v2 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(50));
            Assert.False(v1.Equals(v2));
            Assert.True(v1 != v2);
        }

        [Fact]
        public void GetHashCodeTest()
        {
            var v1 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            var v2 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
        }
    }
}