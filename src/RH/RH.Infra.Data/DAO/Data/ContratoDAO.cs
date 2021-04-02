using RH.Domain.Colaboradores;
using RH.Infra.Data.Context;
using RH.Infra.Data.DAO.Contracts;

namespace RH.Infra.Data.DAO.Data
{
    public class ContratoDao : GenericDao<Contrato>, IContratoDao
    {
        public ContratoDao(CodeiziContext db)
            : base(db) { }
    }
}