using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using System;
using System.Collections.Generic;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Events
{
    public class ColaboradorEventSource : Event
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string ObservacaoContratual { get; set; }
        public List<ContratoEventSource> Contratos { get; set; }

        protected ColaboradorEventSource()
        {
        }

        public ColaboradorEventSource(Colaborador colaborador)
        {
            AggregateId = colaborador.Id.ToString();
            Nome = colaborador.Nome.Nome;
            Sobrenome = colaborador.Nome.Sobrenome;
            DataNascimento = colaborador.DataDeNascimento;
            ObservacaoContratual = colaborador.ObservacaoContratual;
            Contratos = new List<ContratoEventSource>();
            foreach (var contrato in colaborador.Contratos)
            {
                Contratos.Add(new ContratoEventSource
                {
                    DataFim = contrato.DataFim,
                    DataInicio = contrato.DataInicio,
                    SalarioContratual = contrato.SalarioContratual,
                });
            }
        }
    }
}