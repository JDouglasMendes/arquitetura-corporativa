using RH.Domain.Ocorrencias.Ferias;
using RH.Infra.Data.Context;
using RH.Infra.Data.DAO.Contracts;

namespace RH.Infra.Data.DAO.Data
{
    public class OcorrenciaDeFeriasDAO : GenericDAO<OcorrenciaDeFerias>, IOcorrenciaDeFeriasDAO
    {
        public OcorrenciaDeFeriasDAO(CodeiziContext db)
            : base(db)
        { }

    }
}