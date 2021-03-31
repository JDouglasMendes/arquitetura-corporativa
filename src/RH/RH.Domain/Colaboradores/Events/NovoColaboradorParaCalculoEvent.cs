using Domain.SharedKernel.Events;
using System;
using System.Linq;

namespace RH.Domain.Colaboradores.Events
{
    public class NovoColaboradorParaCalculoEvent : Event
    {
        public NovoColaboradorParaCalculoEvent(Guid idColaborador, Guid idContrato, DateTime dataInicio, DateTime? dataFim, double salarioContratual)
        {
            AggregateId = idColaborador;
            IdColaborador = idColaborador;
            IdContrato = idContrato;
            DataInicio = dataInicio;
            DataFim = dataFim;
            SalarioContratual = salarioContratual;
        }

        public static NovoColaboradorParaCalculoEvent Crie(Colaborador colaborador)
            => new NovoColaboradorParaCalculoEvent(colaborador.Id,
                                                   colaborador.Contratos.First().Id,
                                                   colaborador.Contratos.First().DataInicio,
                                                   colaborador.Contratos.First().DataFim,
                                                   colaborador.Contratos.First().SalarioContratual);

        public Guid IdColaborador { get; private set; }
        public Guid IdContrato { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }
        public double SalarioContratual { get; private set; }
    }
}