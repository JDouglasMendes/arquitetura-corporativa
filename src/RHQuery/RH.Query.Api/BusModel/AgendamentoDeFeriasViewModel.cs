using System;

namespace RH.Query.BusModel
{
    public class AgendamentoDeFeriasViewModel
    {
        public const string ColletionName = "AgendamentoDeFeriasQuery";

        public Guid IdColaborador { get; set; }
        public Guid IdContrato { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataInicio { get; set; }
        public byte DiasDeFerias { get; set; }
        public byte DiasDeAbono { get; set; }
        public DateTime PeriodoAquisitivo { get; set; }
        public bool FeriasParceladas { get; set; }
        public DateTime DataFim { get; set; }
    }
}