using RH.Domain.Colaboradores;
using RH.Infra.Data.Context;
using RH.Infra.Data.DAO.Contracts;

namespace RH.Infra.Data.DAO.Data
{
    public class ColaboradorDao : GenericDao<Colaborador>, IColaboradorDAO
    {
        public ColaboradorDao(CodeiziContext db)
            : base(db) { }
    }
}