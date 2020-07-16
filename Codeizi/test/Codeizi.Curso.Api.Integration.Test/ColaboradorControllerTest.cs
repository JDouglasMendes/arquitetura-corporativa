using Codeizi.Curso.Api.Integration.Test.Cenarios;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Curso.Api.Integration.Test
{
    public class ColaboradorControllerTest : BaseIntegrationTest
    {
        [Fact]
        public async Task RealizeAdmissaoAsync()
        {
            var colaboradorAdmissao = ColaboradorViewModelBuilder.CrieAdmissaoSucesso();
            var response = await Client.PostAsync("/api/colaborador/realize-admissao", colaboradorAdmissao.ToJson());
            response.EnsureSuccessStatusCode();
            dynamic resultadoAdmissao = await JsonToObject<object>.Convert(response);
            Assert.True(resultadoAdmissao.data != null);
        }

        [Fact]
        public async Task RealiseAdmissaoComErroDeDados()
        {
            var colaboradorAdmissao = ColaboradorViewModelBuilder.CrieAdmissaoSucesso();
            colaboradorAdmissao.Nome = string.Empty;
            var response = await Client.PostAsync("/api/colaborador/realize-admissao", colaboradorAdmissao.ToJson());
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var resultadoAdmissao = await JsonToObject<ErrorBadRequestJson>.Convert(response);
            Assert.Equal("Nome", ((System.Collections.Generic.IDictionary<string, Newtonsoft.Json.Linq.JToken>)resultadoAdmissao.Errors).Keys.FirstOrDefault());
        }

        [Fact]
        public async Task AdmissaoComColaboradorContratoInconsistente()
        {
            var colaboradorAdmissao = ColaboradorViewModelBuilder.CrieAdmissaoSucesso();
            colaboradorAdmissao.DataNascimento = DateTime.Now;
            var response = await Client.PostAsync("/api/colaborador/realize-admissao", colaboradorAdmissao.ToJson());
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}