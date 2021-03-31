using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using System;
using System.Collections.Generic;
using Xunit;

namespace CalculoFolhaDePagamento.Test.Domain
{
    public class ValorComponenteCalculoTest
    {
        [Theory]
        [MemberData(nameof(CenarioSobreCargaOperador))]
        public static void SobrecargaOperadorTest(ValorComponenteCalculo p1, ValorComponenteCalculo p2, Func<ValorComponenteCalculo, ValorComponenteCalculo, bool> func, bool result)
            => Assert.True(func(p1, p2) == result);

        private static readonly Func<ValorComponenteCalculo, ValorComponenteCalculo, bool> maior = (x, y) => x > y;
        private static readonly Func<ValorComponenteCalculo, ValorComponenteCalculo, bool> menor = (x, y) => x < y;
        private static readonly Func<ValorComponenteCalculo, ValorComponenteCalculo, bool> igual = (x, y) => x == y;
        private static readonly Func<ValorComponenteCalculo, ValorComponenteCalculo, bool> diferente = (x, y) => x != y;
        private static readonly Func<ValorComponenteCalculo, ValorComponenteCalculo, bool> maiorIgual = (x, y) => x >= y;
        private static readonly Func<ValorComponenteCalculo, ValorComponenteCalculo, bool> menorIgual = (x, y) => x <= y;

        public static IEnumerable<object[]> CenarioSobreCargaOperador =>
                                    new List<object[]>
                                    {
                                        new object[] { new ValorComponenteCalculo(1), ValorComponenteCalculo.Zero, maior, true },
                                        new object[] { ValorComponenteCalculo.Zero,  new ValorComponenteCalculo(1), menor, true },
                                        new object[] { ValorComponenteCalculo.Zero, ValorComponenteCalculo.Zero, igual, true },
                                        new object[] { ValorComponenteCalculo.Zero, (ValorComponenteCalculo)1, diferente, true },
                                        new object[] { (ValorComponenteCalculo)100, (ValorComponenteCalculo)100, maiorIgual, true },
                                        new object[] { (ValorComponenteCalculo)100, (ValorComponenteCalculo)100, menorIgual, true },

                                        new object[] { (ValorComponenteCalculo)100, (ValorComponenteCalculo)1000, maior, false },
                                        new object[] { (ValorComponenteCalculo)100, (ValorComponenteCalculo)1, menor, false },
                                        new object[] { (ValorComponenteCalculo)100, (ValorComponenteCalculo)10, igual, false },
                                        new object[] { (ValorComponenteCalculo)100, (ValorComponenteCalculo)100, diferente, false },
                                        new object[] { (ValorComponenteCalculo)10, (ValorComponenteCalculo)100, maiorIgual, false },
                                        new object[] { (ValorComponenteCalculo)100, (ValorComponenteCalculo)1, menorIgual, false },
                                    };

        [Theory]
        [InlineData(10.5333)]
        public void SobrecargaOperadorAritmetica(double result)
        {
            Assert.Equal(result + result, (new ValorComponenteCalculo(result) + new ValorComponenteCalculo(result)).Valor);
            Assert.Equal(result - result, (new ValorComponenteCalculo(result) - new ValorComponenteCalculo(result)).Valor);
            Assert.Equal(result * result, (new ValorComponenteCalculo(result) * new ValorComponenteCalculo(result)).Valor);
            Assert.Equal(result / result, (new ValorComponenteCalculo(result) / new ValorComponenteCalculo(result)).Valor);
        }

        [Fact]
        public void CompareToTest()
        {
            Assert.True(((ValorComponenteCalculo)100).CompareTo((ValorComponenteCalculo)1) > 0);
            Assert.True(((ValorComponenteCalculo)100).CompareTo((ValorComponenteCalculo)100) == 0);
            Assert.True(((ValorComponenteCalculo)100).CompareTo((ValorComponenteCalculo)1000) < 0);
        }

        [Fact]
        public void EqualsObjectTest()
        {
            var v1 = (ValorComponenteCalculo)100;
            var v2 = (ValorComponenteCalculo)100;
            v2.Equals((object)v1);
        }

        [Theory]
        [InlineData(100, 100, true)]
        [InlineData(100, 1000, false)]
        public void GetHashCodeTest(double v1, double v2, bool result)
            => Assert.True(((ValorComponenteCalculo)v1).GetHashCode() == ((ValorComponenteCalculo)v2).GetHashCode() == result);
    }
}