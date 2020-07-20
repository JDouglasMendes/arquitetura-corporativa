using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.BusModel
{
    [ExcludeFromCodeCoverage]
    public class ContratoBusModel
    {
        public Guid IdColaborador { get; set; }
        public Guid IdContrato { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public double SalarioContratual { get; set; }
    }
}