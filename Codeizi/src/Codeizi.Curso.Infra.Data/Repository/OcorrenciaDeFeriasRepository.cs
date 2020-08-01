using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using Codeizi.Curso.RH.Infra.Data.DAO.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Infra.Data.Repository
{
    public class OcorrenciaDeFeriasRepository : IOcorrenciaDeDeriasRepository
    {
        private readonly IOcorrenciaDeFeriasDAO _ocorrenciaDeFeriasDAO;

        public OcorrenciaDeFeriasRepository(IOcorrenciaDeFeriasDAO ocorrenciaDeFeriasDAO)
            => _ocorrenciaDeFeriasDAO = ocorrenciaDeFeriasDAO;

        public async Task<List<OcorrenciaDeFerias>> ObtenhaOcorrenciasDoPeriodoAquisitivo(Guid idContrato, DateTime periodoAquisitivo)
            => await _ocorrenciaDeFeriasDAO
                    .GetAll()
            .Where(x => x.ContradoId == idContrato && x.PeriodoArquisitivo == periodoAquisitivo)
            .ToListAsync();

        public async Task RegistrarOcorrenciaDeFeriasCommand(OcorrenciaDeFerias ocorrenciaDeFerias)
            => await _ocorrenciaDeFeriasDAO.AddAsync(ocorrenciaDeFerias);
    }
}