using RH.Domain.Ocorrencias.Ferias.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Domain.Ocorrencias.Ferias.Contracts
{
    public interface IOcorrenciaDeDeriasRepository
    {
        Task<List<OcorrenciaDeFerias>> ObtenhaOcorrenciasDoPeriodoAquisitivo(Guid idContrato, DateTime periodoAquisitivo);
        Task RegistrarOcorrenciaDeFeriasCommand(OcorrenciaDeFerias ocorrenciaDeFerias);
    }
}