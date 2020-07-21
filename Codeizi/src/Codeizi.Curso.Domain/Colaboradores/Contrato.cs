using Codeizi.Curso.RH.Domain.SharedKernel.Entities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.RH.Domain.Colaboradores
{
    public class Contrato : Entity
    {
        public Contrato(Colaborador colaborador, DateTime dataInicio, double salarioContratual)
        {
            DataInicio = dataInicio;
            SalarioContratual = salarioContratual;
            Colaborador = colaborador;
        }

        [ExcludeFromCodeCoverage]
        protected Contrato() { }

        [ExcludeFromCodeCoverage]
        public Colaborador Colaborador { get; }

        [ExcludeFromCodeCoverage]
        public Guid ColaboradorId => Colaborador.Id;

        public DateTime DataInicio { get; }

        public DateTime? DataFim { get; set; }

        public double SalarioContratual { get; }

        public void EncerreContrato(DateTime date)
            => DataFim = date;

        public bool ContratoAindaVigente => DataFim == default;
    }
}