using Api.Integration.Test.Cenarios;
using Domain.SharedKernel.ValueObjects;
using RH.Domain.Colaboradores;
using RH.Domain.Ocorrencias.Ferias;
using RH.Infra.Data.DAO.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.Controllers
{
    [Trait("Categoria", "Integration")]
    public class OcorrenciaDeFeriasControllerTest : BaseIntegrationTest
    {
        [Fact]
        public async Task Registre_ferias_com_sucesso()
        {
            // Arrange
            var context = InMemoryDBContext.Crie();
            var colaboradorDAO = FabricGenericDAO<IColaboradorDAO>.Crie(context);

            var colaborador = new Colaborador(Guid.NewGuid(), NomePessoa.Crie("Codeizi", "Treinamento"), DateTime.Now.AddYears(-34));
            colaborador.AddContrato(DateTime.Now.AddYears(-1), 1000);

            await context.AddAsync(colaborador);

            await context.SaveChangesAsync();

            var colaboradorSalvo = colaboradorDAO.GetQueryable().FirstOrDefault(x => x.Id == colaborador.Id);

            var registroDeFerias = RegistrarOcorrenciaDeFeriasViewModelBuilder.CrieConsiderandoContrato(colaboradorSalvo.Contratos.First());

            // act
            var response = await Client.PostAsync("api/ocorrenciadeferias", registroDeFerias.ToJson());
            response.EnsureSuccessStatusCode();
            var resultado = await JsonToObject<Resposta>.Convert(response);

            // assert
            Assert.True(resultado.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Registro_de_ferias_sem_saldo_de_ferias()
        {
            // Arrange
            var context = InMemoryDBContext.Crie();
            var contratoDAO = FabricGenericDAO<IContratoDAO>.Crie(context);
            var contrato = new Contrato(
                new Colaborador(Guid.NewGuid(), NomePessoa.Crie("Codeizi", "Treinamento"), DateTime.Now.AddYears(-34)),
                DateTime.Now.AddYears(-1),
                1000);

            await contratoDAO.AddAsync(contrato);

            await context.SaveChangesAsync();

            var registroDeFeriasCadastrado = new OcorrenciaDeFerias(contrato, contrato.PeriodoAquisitivo, 30, 0);

            var feriasDAO = FabricGenericDAO<IOcorrenciaDeFeriasDAO>.Crie(context);

            await feriasDAO.AddAsync(registroDeFeriasCadastrado);

            await context.SaveChangesAsync();

            var registroDeFerias = RegistrarOcorrenciaDeFeriasViewModelBuilder.CrieConsiderandoContrato(contrato);

            // Act
            var response = await Client.PostAsync("api/ocorrenciadeferias", registroDeFerias.ToJson());
            var resultado = await JsonToObject<Resposta>.Convert(response);

            // Assert
            Assert.False(resultado.Success);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(resultado.Errors.Any());
        }
    }
}