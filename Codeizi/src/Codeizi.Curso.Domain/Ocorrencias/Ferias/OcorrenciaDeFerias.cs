using Codeizi.Curso.RH.Domain.Colaboradores;
using Codeizi.Curso.RH.Domain.SharedKernel.Entities;
using System;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias
{
    public class OcorrenciaDeFerias : Entity
    {
        public const byte QuantidadeMaximaDeDiasDeFerias = 30;
        public const byte QuantidadeMaximaDeDiasDeAbono = 10;

        public OcorrenciaDeFerias(Contrato contrato, DateTime dataDeInicio, byte diasDeFerias, byte diasDeAbono)
        {
            Contrato = contrato;
            DataDeInicio = dataDeInicio;
            DiasDeFerias = diasDeFerias;
            DiasDeAbono = diasDeAbono;
            PeriodoArquisitivo = contrato.PeriodoAquisitivo;
        }

        protected OcorrenciaDeFerias() { }

        public Contrato Contrato { get; private set; }
        public Guid ContradoId { get; private set; }
        public DateTime DataDeInicio { get; private set; }
        public DateTime DataFim => DataDeInicio.AddDays(DiasDeFerias);
        public byte DiasDeFerias { get; private set; }
        public byte DiasDeAbono { get; private set; }
        public DateTime PeriodoArquisitivo { get; private set; }
        public bool FeriasParcelada => (DiasDeFerias + DiasDeAbono) < QuantidadeMaximaDeDiasDeFerias;
        public short DiasDeFeriasTotais => (short)(DiasDeFerias + DiasDeAbono);
    }
}