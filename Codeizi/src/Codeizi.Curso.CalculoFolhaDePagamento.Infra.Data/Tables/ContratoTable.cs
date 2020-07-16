using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Tables
{
    public class ContratoTable
    {
        public Guid Id { get; set; }
        public Guid IdColaborador { get; set; }
        public Guid IdContrato { get; set; }
        public int DataInicio { get; set; }
        public int? DataFim { get; set; }
        public double SalarioContratual { get; set; }
    }
}