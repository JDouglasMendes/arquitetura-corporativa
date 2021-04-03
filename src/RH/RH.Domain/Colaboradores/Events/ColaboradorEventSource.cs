using Domain.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RH.Domain.Colaboradores.Events
{
    public class ColaboradorEventSource : Event
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string ObservacaoContratual { get; set; }
        public List<ContratoEventSource> Contratos { get; set; }
        public override string GetKeyQueues => "colaborador-event-source";

        public static ColaboradorEventSource Crie(Colaborador colaborador)
            => new ColaboradorEventSource
            {
                AggregateId = colaborador.Id,
                Nome = colaborador.Nome.Nome,
                Sobrenome = colaborador.Nome.Sobrenome,
                DataNascimento = colaborador.DataDeNascimento,
                ObservacaoContratual = colaborador.ObservacaoContratual,
                Contratos = colaborador
                            .Contratos
                            .ToList()
                            .ConvertAll(new Converter<Contrato, ContratoEventSource>(x => new ContratoEventSource
                            {
                                DataInicio = x.DataInicio,
                                DataFim = x.DataFim,
                                SalarioContratual = x.SalarioContratual,
                            })),
            };
    }
}