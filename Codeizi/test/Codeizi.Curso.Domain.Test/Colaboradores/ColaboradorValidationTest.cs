using Codeizi.Curso.Domain.Colaboradores.Commands;
using System;
using System.Collections.Generic;
using System.Xml.Xsl;
using Xunit;

namespace Codeizi.Curso.Domain.Test.Colaboradores
{
    public class ColaboradorValidationTest
    {
        [Theory]
        [InlineData("Codeizi", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("a", false)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false)]
        public void ValideNomeTest(string nome, bool valido)
        {
            var colaborador = new AdmissaoColaboradorCommand(nome, "Sobrenome", DateTime.Now, 1000, DateTime.Now.AddYears(-50));
            colaborador.IsValid();
            Assert.Equal(valido, colaborador.ValidationResult.IsValid);
        }

        [Theory]
        [InlineData("Codeizi", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("a", true)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false)]
        public void ValideSobrenomeTest(string sobrenome, bool valido)
        {
            var colaborador = new AdmissaoColaboradorCommand("Codeizi", sobrenome, DateTime.Now, 1000, DateTime.Now.AddYears(-50));
            colaborador.IsValid();
            Assert.Equal(valido, colaborador.ValidationResult.IsValid);
        }
        [Theory]
        [MemberData(nameof(DataAdmissaoParaTestes))]
        public void ValideDataAdmisaoColaboradorTest(DateTime dataAdmissao, bool valido)
        {
            var colaborador = new AdmissaoColaboradorCommand("Codeizi", "Treinamento", dataAdmissao, 1000, DateTime.Now.AddYears(-50));
            colaborador.IsValid();
            Assert.Equal(valido, colaborador.ValidationResult.IsValid);
        }

        public static IEnumerable<object[]> DataAdmissaoParaTestes =>
                                                                    new List<object[]>
                                                                    {
                                                                        new object[] { DateTime.Now, true },
                                                                        new object[] { default(DateTime), false },
                                                                    };

        [Theory]
        [InlineData(0, false)]
        [InlineData(double.MinValue, false)]
        [InlineData(1, true)]
        public void ValideSalarioContratual(double salarioContratual, bool valido)
        {
            var colaborador = new AdmissaoColaboradorCommand("Codeizi", "Treinamento", DateTime.Now, salarioContratual, DateTime.Now.AddYears(-50));
            colaborador.IsValid();
            Assert.Equal(valido, colaborador.ValidationResult.IsValid);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("Codeizi", true)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false)]
        public void ValideObservacaoContratual(string observacaoContratual, bool valido)
        {
            var colaborador = new AdmissaoColaboradorCommand("Codeizi", "Treinamento", DateTime.Now, 10, DateTime.Now.AddYears(-50))
            {
                ObservacaoContratual = observacaoContratual,
            };
            colaborador.IsValid();
            Assert.Equal(valido, colaborador.ValidationResult.IsValid);
        }

        [Fact]
        public void ValideDataAdmisaoAntesDataNascimentoTest()
        {
            var colaborador = new AdmissaoColaboradorCommand("Codeizi", "Treinamento", DateTime.Now, 1000, DateTime.Now.AddYears(-50))
            {
                ObservacaoContratual = "Obs",
            };
            colaborador.IsValid();
            Assert.True(colaborador.ValidationResult.IsValid);
        }

        [Fact]
        public void ValideDataAdmisaoDepoisDataNascimentoTest()
        {
            var colaborador = new AdmissaoColaboradorCommand("Codeizi", "Treinamento", DateTime.Now, 1000, DateTime.Now.AddYears(50))
            {
                ObservacaoContratual = "Obs",
            };
            colaborador.IsValid();
            Assert.False(colaborador.ValidationResult.IsValid);
        }

    }
}
