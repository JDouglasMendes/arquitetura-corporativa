using Codeizi.Curso.RH.Infra.Data.DAO.Contracts;
using Codeizi.Curso.RH.Infra.Data.Repository;
using Codeizi.Infra.Data.Test.Cenarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Codeizi.Infra.Data.Test.Repository
{
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
            Assert.True(await colaboradorDAO.GetAll().CountAsync() > 0, "Não existe colaborador cadastrado");
            Assert.True(await contratoDAO.GetAll().CountAsync() > 0, "Não existe contrato cadastrado");
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
    }
}