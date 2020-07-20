using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using System;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Events
{
    public class ColaboradorAdmitidoEvent : Event
    {
        public ColaboradorAdmitidoEvent(Guid id, string nome, string sobrenome, DateTime dataDeAdmissao, double salarioContratual, string observacaoContratual)
        {
            AggregateId = id;
            Nome = nome;
            Sobrenome = sobrenome;
            DataDeAdmissao = dataDeAdmissao;
            SalarioContratual = salarioContratual;
            ObservacaoContratual = observacaoContratual;
        }

        public string Nome { get; }
        public string Sobrenome { get; }
        public DateTime DataDeAdmissao { get; }
        public double SalarioContratual { get; }
        public string ObservacaoContratual { get; }
    }
}