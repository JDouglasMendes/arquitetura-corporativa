using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;
using Xunit;

namespace CalculoFolhaDePagamento.Test.Domain
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
        public void Diferenca_vigencias()
        {
            var v1 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            var v2 = new Vigencia(new DateTime(2029, 1, 1), new DateTime(2030, 1, 1).AddDays(100));
            Assert.False(v1.Equals(v2));
            Assert.False(v1 == v2);
            Assert.False(((object)v1).Equals(v2));
        }

        [Fact]
        public void Diferenca_vigencias_obj()
        {
            var v1 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            var v2 = (object)new Vigencia(new DateTime(2029, 1, 1), new DateTime(2030, 1, 1).AddDays(100));
            Assert.False(v1.Equals(v2));
        }

        [Theory]
        [InlineData(2020, 1, 1, 100, 100, true)]
        [InlineData(2020, 1, 1, 100, 99, false)]
        public void Compare_vigencias(
            int ano,
            int mes,
            int dia,
            int addDays,
            int addDaysFinish,
            bool result)
        {
            var v1 = new Vigencia(new DateTime(ano, mes, dia), new DateTime(ano, mes, dia).AddDays(addDays));
            var v2 = new Vigencia(new DateTime(ano, mes, dia), new DateTime(ano, mes, dia).AddDays(addDaysFinish));
            Assert.Equal(result, v1.Equals(v2));
        }

        [Theory]
        [InlineData(2020, 1, 1, 100, 100, true)]
        [InlineData(2020, 1, 1, 100, 99, false)]
        public void Compare_vigencias_obj(
                    int ano,
                    int mes,
                    int dia,
                    int addDays,
                    int addDaysFinish,
                    bool result)
        {
            var v1 = new Vigencia(new DateTime(ano, mes, dia), new DateTime(ano, mes, dia).AddDays(addDays));
            var v2 = (object) new Vigencia(new DateTime(ano, mes, dia), new DateTime(ano, mes, dia).AddDays(addDaysFinish));
            Assert.Equal(result, v1.Equals(v2));
        }

        [Fact]
        public void Compare_outro_tipo_objeto()
        {
            var v1 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            var v2 = new Vigencia(new DateTime(2020, 1, 1), new DateTime(2020, 1, 1).AddDays(100));
            Assert.False(v1.Equals(new object()));
            Assert.True(v1.Equals((object)v2));
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