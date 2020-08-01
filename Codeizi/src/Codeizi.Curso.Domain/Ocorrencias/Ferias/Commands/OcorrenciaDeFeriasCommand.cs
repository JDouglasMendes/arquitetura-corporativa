using Codeizi.Curso.RH.Domain.SharedKernel.Commands;
using System;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands
{
    public abstract class OcorrenciaDeFeriasCommand : Command
    {
        public Guid IdColaborador { get; set; }
        public Guid IdContrato { get; set; }
        public DateTime PeriodoAquisitivo { get; set; }
        public DateTime DataDeInicio { get; set; }
        public byte DiasDeFerias { get; set; }
        public byte DiasDeAbono { get; set; }
    }
}