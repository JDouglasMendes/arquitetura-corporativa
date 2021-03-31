using RH.Domain.Ocorrencias.Ferias;
using RH.Infra.Data.DAO.Contracts;
using RH.Infra.Data.Repository;
using Codeizi.Infra.Data.Test.Cenarios;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Infra.Data.Test.Repository
{
    [Trait("Categoria", "Data")]
    public class OcorrenciaDeFeriasRepositoryTest
    {
        [Fact]
        public async Task Selecionar_ocorrencia_de_um_contrato()
        {
            // Arrange
            var contexto = new InMemoryDBContext().Crie();
            var colaborador = new ColaboradorBuilder().CrieColaboradorSucesso().Get;
            var contratoDAO = FabricGenericDAO<IContratoDAO>.Crie(contexto);
            var colaboradorDAO = FabricGenericDAO<IColaboradorDAO>.Crie(contexto);
            var ocorrenciaDeFeriasDAO = FabricGenericDAO<IOcorrenciaDeFeriasDAO>.Crie(contexto);
            var repository = new ColaboradorRepository(contratoDAO, colaboradorDAO);
            await repository.RealizeAdmissao(colaborador);

            var ferias = new OcorrenciaDeFerias(colaborador.Contratos.First(),
                                                colaborador.Contratos.First().DataInicio.AddYears(1),
                                                30,
                                                0);

            await ocorrenciaDeFeriasDAO.AddAsync(ferias);
            await contexto.SaveChangesAsync();

            var ocorrenciaFeriasRepository = new OcorrenciaDeFeriasRepository(ocorrenciaDeFeriasDAO);

            // act
            var result = await ocorrenciaFeriasRepository.ObtenhaOcorrenciasDoPeriodoAquisitivo(colaborador.Contratos.First().Id,
                                                                                          colaborador.Contratos.First().PeriodoAquisitivo);
            // Assert
            Assert.NotNull(result);
        }
    }
}