using Codeizi.Curso.FunctionalTest.Helps;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Codeizi.Curso.FunctionalTest
{
    public class CadastrarColaborador
    {
        private readonly HttpClient httpClient;

        public CadastrarColaborador(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CadastrarColaboradorValidos(string args)
        {
            try
            {
                var colaborador = new ColaboradorViewModel
                {
                    DataDeAdmissao = DateTime.Now,
                    DataNascimento = DateTime.Now.AddYears(-30),
                    Nome = "Nome ",
                    ObservacaoContratual = "Obs",
                    SalarioContratual = 1000,
                    Sobrenome = "Sobrenome",
                };

                var response = await httpClient.PostAsync("http://localhost:5010/api/colaborador/realize-admissao", colaborador.ToJson());
                response.EnsureSuccessStatusCode();
                dynamic resultadoAdmissao = await JsonToObject<object>.Convert(response);
            }
            catch (Exception ex)
            {
                ex.Message.Red().WL();
            }
            
        }
    }
}
