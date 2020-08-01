using System;

namespace Codeizi.Curso.RH.Query.Api.BusModel
{
    public class ContratoQueryViewModel
    {
        public const string ColletionName = "ContratoQuery";
        public Guid IdColaborador { get; set; }
        public Guid IdContrato { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string ObservacaoContratual { get; set; }
        public DateTime DataInicioContrato { get; set; }
        public DateTime? DataFimContrato { get; set; }
        public double SalarioContratual { get; set; }
    }
}