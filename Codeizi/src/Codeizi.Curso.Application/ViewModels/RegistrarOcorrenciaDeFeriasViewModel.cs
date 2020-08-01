using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.RH.Application.ViewModels
{
    public class RegistrarOcorrenciaDeFeriasViewModel
    {
        public Guid IdContrato { get; set; }
        public DateTime PeriodoAquisitivo { get; set; }
        public DateTime DataDeInicio { get; set; }
        public byte DiasDeFerias { get; set; }
        public byte DiasDeAbono { get; set; }
    }
}
