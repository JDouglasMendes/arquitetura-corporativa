using Codeizi.Curso.Domain.Colaboradores;
using Codeizi.Curso.Infra.Data.Context;
using Codeizi.Curso.Infra.Data.DAO.Contracts;

namespace Codeizi.Curso.Infra.Data.DAO.Data
{
    public class ContratoDAO : GenericDAO<Contrato>, IContratoDAO
    {
        public ContratoDAO(CodeiziContext db)
            : base(db) { }
    }
}