using Codeizi.Curso.Domain.Colaboradores;
using Codeizi.Curso.Infra.Data.Context;
using Codeizi.Curso.Infra.Data.DAO.Contracts;

namespace Codeizi.Curso.Infra.Data.DAO.Data
{
    public class ColaboradorDAO : GenericDAO<Colaborador>, IColaboradorDAO
    {
        public ColaboradorDAO(CodeiziContext db)
            : base(db) { }
    }
}
