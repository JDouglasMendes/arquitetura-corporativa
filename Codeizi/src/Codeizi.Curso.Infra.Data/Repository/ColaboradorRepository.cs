﻿using Codeizi.Curso.RH.Domain.Colaboradores;
using Codeizi.Curso.RH.Domain.Colaboradores.Contracts;
using Codeizi.Curso.RH.Infra.Data.DAO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Infra.Data.Repository
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly IContratoDAO contratoDAO;
        private readonly IColaboradorDAO colaboradorDAO;

        public ColaboradorRepository(IContratoDAO contratoDAO, IColaboradorDAO colaboradorDAO)
        {
            this.contratoDAO = contratoDAO;
            this.colaboradorDAO = colaboradorDAO;
        }

        public async Task<Colaborador> BusqueColaborador(Guid idColaborador)
            => await colaboradorDAO.GetByIdAsync(idColaborador);

        public IEnumerable<Contrato> BusqueTodosContratos(Guid guid)
            => contratoDAO.GetQueryable().AsEnumerable().Where(x => x.ColaboradorId == guid).ToList();

        public async Task<Contrato> ObtenhaContrato(Guid idContrato)
            => await contratoDAO.GetByIdAsync(idContrato);

        public async Task RealizeAdmissao(Colaborador colaborador)
            => await colaboradorDAO.AddAsync(colaborador);
    }
}