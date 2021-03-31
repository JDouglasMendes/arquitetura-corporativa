﻿using RH.Domain.Colaboradores;
using RH.Infra.Data.Context;
using RH.Infra.Data.DAO.Contracts;

namespace RH.Infra.Data.DAO.Data
{
    public class ContratoDAO : GenericDAO<Contrato>, IContratoDAO
    {
        public ContratoDAO(CodeiziContext db)
            : base(db) { }
    }
}