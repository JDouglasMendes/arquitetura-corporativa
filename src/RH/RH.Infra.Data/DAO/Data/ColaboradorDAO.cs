using RH.Domain.Colaboradores;
using RH.Infra.Data.Context;
using RH.Infra.Data.DAO.Contracts;

namespace RH.Infra.Data.DAO.Data
{
    public class ColaboradorDAO : GenericDAO<Colaborador>, IColaboradorDAO
    {
        public ColaboradorDAO(CodeiziContext db)
            : base(db) { }
    }
}