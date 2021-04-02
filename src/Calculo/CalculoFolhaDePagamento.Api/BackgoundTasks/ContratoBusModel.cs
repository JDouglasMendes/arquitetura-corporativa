using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculoFolhaDePagamento.Api.BackgoundTasks
{
    public class ContratoBusModel
    {
        public Guid IdColaborador { get; set; }
        public Guid IdContrato { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public double SalarioContratual { get; set; }
    }
}
