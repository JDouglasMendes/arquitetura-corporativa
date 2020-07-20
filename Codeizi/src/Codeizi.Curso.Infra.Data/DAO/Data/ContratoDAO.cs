using Codeizi.Curso.RH.Domain.Colaboradores;
using Codeizi.Curso.RH.Infra.Data.Context;
using Codeizi.Curso.RH.Infra.Data.DAO.Contracts;

namespace Codeizi.Curso.RH.Infra.Data.DAO.Data
{
    public class ContratoDAO : GenericDAO<Contrato>, IContratoDAO
    {
        public ContratoDAO(CodeiziContext db)
            : base(db) { }
    }
}