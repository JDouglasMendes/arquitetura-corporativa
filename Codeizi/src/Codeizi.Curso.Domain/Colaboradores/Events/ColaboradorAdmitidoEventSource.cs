using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using System;
using System.Linq;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Events
{
    public class ColaboradorAdmitidoEventSource : Event
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataDeAdmissao { get; set; }
        public double SalarioContratual { get; set; }
        public string ObservacaoContratual { get; set; }

        public ColaboradorAdmitidoEventSource()
        {
        }

        public ColaboradorAdmitidoEventSource(Colaborador colaborador)
        {
            Nome = colaborador.Nome.Nome;
            Sobrenome = colaborador.Nome.Sobrenome;
            DataNascimento = colaborador.DataDeNascimento;
            var contrato = colaborador.Contratos.ToList().First();
            DataDeAdmissao = contrato.DataInicio;
            SalarioContratual = contrato.SalarioContratual;
            ObservacaoContratual = colaborador.ObservacaoContratual;
        }
    }
}