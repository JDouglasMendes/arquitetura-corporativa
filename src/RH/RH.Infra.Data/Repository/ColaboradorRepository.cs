using RH.Domain.Colaboradores;
using RH.Domain.Colaboradores.Contracts;
using RH.Infra.Data.DAO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Infra.Data.Repository
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly IContratoDao contratoDAO;
        private readonly IColaboradorDAO colaboradorDAO;

        public ColaboradorRepository(IContratoDao contratoDAO, IColaboradorDAO colaboradorDAO)
        {
            this.contratoDAO = contratoDAO;
            this.colaboradorDAO = colaboradorDAO;
        }

        public async Task<Colaborador> BusqueColaborador(Guid idColaborador)
            => await colaboradorDAO.GetByIdAsync(idColaborador);

        public IEnumerable<Contrato> BusqueTodosContratos(Guid idColaborador)
            => contratoDAO.GetQueryable().AsEnumerable().Where(x => x.ColaboradorId == idColaborador).ToList();

        public async Task<Contrato> ObtenhaContrato(Guid idContrato)
            => await contratoDAO.GetByIdAsync(idContrato);

        public async Task RealizeAdmissao(Colaborador colaborador)
            => await colaboradorDAO.AddAsync(colaborador);
    }
}