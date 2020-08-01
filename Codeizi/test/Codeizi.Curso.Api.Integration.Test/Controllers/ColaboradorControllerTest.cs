using Codeizi.Curso.Api.Integration.Test.Cenarios;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Curso.Api.Integration.Test.Controllers
{
    [Trait("Categoria", "Integration")]
    public class ColaboradorControllerTest : BaseIntegrationTest
    {
        [Fact]
        public async Task Admitir_colaborador_com_sucesso()
        {
            var colaboradorAdmissao = ColaboradorViewModelBuilder.CrieAdmissaoSucesso();
            var response = await Client.PostAsync("/api/colaborador", colaboradorAdmissao.ToJson());
            response.EnsureSuccessStatusCode();
            var resultadoAdmissao = await JsonToObject<Resposta>.Convert(response);
            Assert.True(resultadoAdmissao.Success);
            Assert.Null(resultadoAdmissao.Errors);
        }

        [Fact]
        public async Task Tentar_admitir_colaborador_sem_nome()
        {
            var colaboradorAdmissao = ColaboradorViewModelBuilder.CrieAdmissaoSucesso();
            colaboradorAdmissao.Nome = string.Empty;
            var response = await Client.PostAsync("/api/colaborador", colaboradorAdmissao.ToJson());
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var resultadoAdmissao = await JsonToObject<ErrorBadRequestJson>.Convert(response);
            Assert.Equal("Nome", ((System.Collections.Generic.IDictionary<string, Newtonsoft.Json.Linq.JToken>)resultadoAdmissao.Errors).Keys.FirstOrDefault());
        }

        [Fact]
        public async Task Tentar_admitir_colaborador_com_contrato_inconsistente()
        {
            var colaboradorAdmissao = ColaboradorViewModelBuilder.CrieAdmissaoSucesso();
            colaboradorAdmissao.DataNascimento = DateTime.Now;
            var response = await Client.PostAsync("/api/colaborador", colaboradorAdmissao.ToJson());
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}