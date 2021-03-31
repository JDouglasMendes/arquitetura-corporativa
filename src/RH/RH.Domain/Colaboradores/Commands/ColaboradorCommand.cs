using Domain.SharedKernel.Commands;
using System;

namespace RH.Domain.Colaboradores.Commands
{
    public abstract class ColaboradorCommand : Command
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataDeAdmissao { get; set; }
        public double SalarioContratual { get; set; }
        public string ObservacaoContratual { get; set; }
    }
}