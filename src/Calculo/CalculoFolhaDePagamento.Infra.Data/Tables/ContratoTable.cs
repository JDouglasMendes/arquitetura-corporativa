using System;

namespace CalculoFolhaDePagamento.Infra.Data.Tables
{
    public class ContratoTable
    {
        public Guid Id { get; set; }
        public string IdColaborador { get; set; }
        public string IdContrato { get; set; }
        public int DataInicio { get; set; }
        public int? DataFim { get; set; }
        public double SalarioContratual { get; set; }
    }
}