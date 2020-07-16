using System;

namespace Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks.BusModel
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