using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using System;
using System.Linq;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Events
{
    public class ContratoQueryEvent : Event
    {
        public Guid IdColaborador { get; set; }
        public Guid IdContrato { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string ObservacaoContratual { get; set; }
        public DateTime DataInicioContrato { get; set; }
        public DateTime? DataFimContrato { get; set; }
        public double SalarioContratual { get; set; }

        public static ContratoQueryEvent Crie(Colaborador colaborador)
         => new ContratoQueryEvent
         {
             AggregateId = colaborador.Id,
             IdColaborador = colaborador.Id,
             Nome = colaborador.Nome.Nome,
             Sobrenome = colaborador.Nome.Sobrenome,
             ObservacaoContratual = colaborador.ObservacaoContratual,
             DataInicioContrato = colaborador.Contratos.First().DataInicio,
             DataFimContrato = colaborador.Contratos.First().DataFim,
             IdContrato = colaborador.Contratos.First().Id,
             SalarioContratual = colaborador.Contratos.First().SalarioContratual,
             DataNascimento = colaborador.DataDeNascimento,
         };
    }
}