using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias;
using Codeizi.Curso.RH.Infra.Data.Context;
using Codeizi.Curso.RH.Infra.Data.DAO.Contracts;

namespace Codeizi.Curso.RH.Infra.Data.DAO.Data
{
    public class OcorrenciaDeFeriasDAO : GenericDAO<OcorrenciaDeFerias>, IOcorrenciaDeFeriasDAO
    {
        public OcorrenciaDeFeriasDAO(CodeiziContext db)
            : base(db)
        { }

    }
}