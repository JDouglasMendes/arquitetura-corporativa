using RH.Infra.Data.DAO.Contracts;
using RH.Infra.Data.Repository;
using Codeizi.Infra.Data.Test.Cenarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Infra.Data.Test.Repository
{
    [Trait("Categoria", "Data")]
    public class ColaboradorRepositoryTest
    {
        [Fact]
        public async Task RealizeAdmissaoAsync()
        {
            var contexto = new InMemoryDBContext().Crie();
            var colaborador = new ColaboradorBuilder().CrieColaboradorSucesso().Get;
            var contratoDAO = FabricGenericDAO<IContratoDAO>.Crie(contexto);
            var colaboradorDAO = FabricGenericDAO<IColaboradorDAO>.Crie(contexto);
            var repository = new ColaboradorRepository(contratoDAO, colaboradorDAO);
            await repository.RealizeAdmissao(colaborador);
            await contexto.SaveChangesAsync();
            Assert.True(await colaboradorDAO.GetQueryable().CountAsync() > 0, "Não existe colaborador cadastrado");
            Assert.True(await contratoDAO.GetQueryable().CountAsync() > 0, "Não existe contrato cadastrado");
        }

        [Fact]
        public async void BusqueTodosContratos()
        {
            var contexto = new InMemoryDBContext().Crie();
            var contratoDAO = FabricGenericDAO<IContratoDAO>.Crie(contexto);
            var colaboradorDAO = FabricGenericDAO<IColaboradorDAO>.Crie(contexto);
            var repository = new ColaboradorRepository(contratoDAO, colaboradorDAO);
            var chave = Guid.NewGuid();
            var colaborador = new ColaboradorBuilder().CrieColaboradorSucesso(chave).Get;
            await colaboradorDAO.AddAsync(colaborador);
            colaborador = new ColaboradorBuilder().CrieColaboradorSucesso(Guid.NewGuid()).Get;
            await colaboradorDAO.AddAsync(colaborador);
            colaborador = new ColaboradorBuilder().CrieColaboradorSucesso(Guid.NewGuid()).Get;
            await colaboradorDAO.AddAsync(colaborador);
            await contexto.SaveChangesAsync();
            var contratosArmazenados = repository.BusqueTodosContratos(chave);
            contratosArmazenados.ToList().ForEach(item => Assert.Equal(chave, item.ColaboradorId));
            Assert.True(contratosArmazenados.Count() == 1);
        }

        [Fact]
        public async Task Busque_colaborador_existente()
        {
            // Arrange
            var contexto = new InMemoryDBContext().Crie();
            var contratoDAO = FabricGenericDAO<IContratoDAO>.Crie(contexto);
            var colaboradorDAO = FabricGenericDAO<IColaboradorDAO>.Crie(contexto);

            var chave = Guid.NewGuid();

            var colaborador = new ColaboradorBuilder().CrieColaboradorSucesso(chave).Get;

            await contexto.AddAsync(colaborador);

            await contexto.SaveChangesAsync();

            var repository = new ColaboradorRepository(contratoDAO, colaboradorDAO);

            // Act
            var result = await repository.BusqueColaborador(chave);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(chave, result.Id);
            Assert.True(result.Contratos.Any());
        }
    }
}