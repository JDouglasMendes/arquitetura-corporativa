using RH.Domain.Ocorrencias.Ferias;
using RH.Infra.Data.Context;
using RH.Infra.Data.DAO.Contracts;

namespace RH.Infra.Data.DAO.Data
{
    public class OcorrenciaDeFeriasDao : GenericDao<OcorrenciaDeFerias>, IOcorrenciaDeFeriasDao
    {
        public OcorrenciaDeFeriasDao(CodeiziContext db)
            : base(db)
        { }

    }
}