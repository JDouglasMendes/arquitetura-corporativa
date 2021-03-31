using System;

namespace RH.Domain.Colaboradores.Events
{
    public class ContratoEventSource
    {
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public double SalarioContratual { get; set; }
    }
}