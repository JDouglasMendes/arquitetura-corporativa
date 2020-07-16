using Codeizi.Curso.Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using Xunit;

namespace Codeizi.Curso.Domain.SharedKernel.Test.ValueObjects
{
    public class NomePessoaTest
    {
        [Fact]
        public void Crie()
        {
            var nome = NomePessoa.Crie("Codeizi", "Treinamentos");
            Assert.Equal("Codeizi", nome.Nome);
            Assert.Equal("Treinamentos", nome.Sobrenome);
        }

        [Theory]
        [InlineData("Codeizi", "Codeizi", true)]
        [InlineData("Code", "Codeizi", false)]
        public void EqualsTest(string nome1, string nome2, bool resultado)
        {
            var nomeCompleto1 = NomePessoa.Crie(nome1, "Treinamentos");
            var nomeCompleto2 = NomePessoa.Crie(nome2, "Treinamentos");
            Assert.True(nomeCompleto1.Equals(nomeCompleto2) == resultado);
        }

        [Theory]
        [InlineData("Codeizi", "Codeizi", true)]
        [InlineData("Code", "Codeizi", false)]
        public void GetHashCodeTest(string nome1, string nome2, bool resultado)
        {
            var nomeCompleto1 = NomePessoa.Crie(nome1, "Treinamento");
            var nomeCompleto2 = NomePessoa.Crie(nome2, "Treinamento");
            Assert.True(nomeCompleto1.GetHashCode() == nomeCompleto2.GetHashCode() == resultado);
        }

        [Fact]
        public void ToStringTest()
        {
            var nomeCompleto1 = NomePessoa.Crie("Codeizi", "Treinamento");
            var result = nomeCompleto1.ToString();
            Assert.Equal("Codeizi Treinamento", result);
        }

        [Theory]
        [InlineData("Codeizi", "Codeizi", true)]
        [InlineData("Code", "Codeizi", false)]
        public void VerificaOperadorIgualdade(string nome1, string nome2, bool resultado)
        {
            var nomeCompleto1 = NomePessoa.Crie(nome1, "Treinamentos");
            var nomeCompleto2 = NomePessoa.Crie(nome2, "Treinamentos");
            Assert.True(nomeCompleto1 == nomeCompleto2 == resultado);
        }

        [Theory]
        [InlineData("Codeizi", "Codeizi", false)]
        [InlineData("Code", "Codeizi", true)]
        public void VerificaOperadorDiferenca(string nome1, string nome2, bool resultado)
        {
            var nomeCompleto1 = NomePessoa.Crie(nome1, "Treinamentos");
            var nomeCompleto2 = NomePessoa.Crie(nome2, "Treinamentos");
            Assert.True(nomeCompleto1 != nomeCompleto2 == resultado);
        }

        [Theory]
        [MemberData(nameof(CenarioSobreCargaOperador))]
        public static void SobrecargaOperadorTest(NomePessoa p1, NomePessoa p2, Func<NomePessoa, NomePessoa, bool> func, bool result)
            => Assert.True(func(p1, p2) == result);

        private static readonly Func<NomePessoa, NomePessoa, bool> maior = (x, y) => x > y;
        private static readonly Func<NomePessoa, NomePessoa, bool> menor = (x, y) => x < y;
        private static readonly Func<NomePessoa, NomePessoa, bool> igual = (x, y) => x == y;
        private static readonly Func<NomePessoa, NomePessoa, bool> diferente = (x, y) => x != y;
        private static readonly Func<NomePessoa, NomePessoa, bool> maiorIgual = (x, y) => x >= y;
        private static readonly Func<NomePessoa, NomePessoa, bool> menorIgual = (x, y) => x <= y;

        public static IEnumerable<object[]> CenarioSobreCargaOperador =>
                                    new List<object[]>
                                    {
                                        new object[] { NomePessoa.Crie("BBB", "BBB"), NomePessoa.Crie("AAAA","AAA"), maior, true },
                                        new object[] { NomePessoa.Crie("AAAAA", "BB"), NomePessoa.Crie("CCCC","AAA"), menor, true },
                                        new object[] { NomePessoa.Crie("BBB", "BBB"), NomePessoa.Crie("BBB","BBB"), igual, true },
                                        new object[] { NomePessoa.Crie("BBB", "BBB"), NomePessoa.Crie("AAAA","AAA"), diferente, true },
                                        new object[] { NomePessoa.Crie("BBB", "BBB"), NomePessoa.Crie("AAAA", "AAA"), maiorIgual, true },
                                        new object[] { NomePessoa.Crie("AAAA", "BBB"), NomePessoa.Crie("AAAA","CCCC"), menorIgual, true },

                                        new object[] { NomePessoa.Crie("BBB", "BBB"), NomePessoa.Crie("DAAA","AAA"), maior, false },
                                        new object[] { NomePessoa.Crie("DAAAA", "BB"), NomePessoa.Crie("CCCC","AAA"), menor, false },
                                        new object[] { NomePessoa.Crie("ABB", "BBB"), NomePessoa.Crie("BBB","BBB"), igual, false },
                                        new object[] { NomePessoa.Crie("BBB", "BBB"), NomePessoa.Crie("BBB", "BBB"), diferente, false },
                                        new object[] { NomePessoa.Crie("BBB", "BBB"), NomePessoa.Crie("CAAA", "AAA"), maiorIgual, false },
                                        new object[] { NomePessoa.Crie("ADAA", "BBB"), NomePessoa.Crie("AAAA","CCCC"), menorIgual, false },
                                    };
    }
}