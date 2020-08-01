using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias;
using Codeizi.Curso.RH.Domain.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.RH.Domain.Colaboradores
{
    public class Contrato : Entity
    {
        private readonly List<OcorrenciaDeFerias> _ferias = new List<OcorrenciaDeFerias>();
        public Contrato(Colaborador colaborador, DateTime dataInicio, double salarioContratual)
        {
            DataInicio = dataInicio;
            SalarioContratual = salarioContratual;
            Colaborador = colaborador;
            PeriodoAquisitivo = DataInicio;
        }

        [ExcludeFromCodeCoverage]
        protected Contrato() { }

        [ExcludeFromCodeCoverage]
        public Colaborador Colaborador { get; }

        [ExcludeFromCodeCoverage]
        public Guid ColaboradorId { get; private set; }
        public DateTime DataInicio { get; }
        public DateTime? DataFim { get; set; }
        public double SalarioContratual { get; }
        public void EncerreContrato(DateTime date)
            => DataFim = date;
        public bool ContratoAindaVigente => DataFim == default;
        public DateTime PeriodoAquisitivo { get; private set; }
        public IReadOnlyCollection<OcorrenciaDeFerias> Ferias => _ferias;
    }
}