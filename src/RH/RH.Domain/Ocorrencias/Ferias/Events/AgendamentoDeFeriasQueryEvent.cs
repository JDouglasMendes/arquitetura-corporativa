using Domain.SharedKernel.Events;
using RH.Domain.Colaboradores;
using System;

namespace RH.Domain.Ocorrencias.Ferias.Events
{
    public class AgendamentoDeFeriasQueryEvent : Event
    {
        public Guid IdColaborador { get; }
        public Guid IdContrato { get; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; }
        public byte DiasDeFerias { get; }
        public byte DiasDeAbono { get; }
        public DateTime PeriodoAquisitivo { get; }
        public bool FeriasParceladas { get; }
        public DateTime DataFim => DataInicio.AddDays(DiasDeFerias);
        public override string GetKeyQueues => "agenda-ferias-query";

        public AgendamentoDeFeriasQueryEvent(
            Colaborador colaborador,
            Guid idContrato,
            DateTime dataInicio,
            byte diasDeFerias,
            byte diasDeAbono,
            DateTime periodoAquisitivo,
            bool feriasParceladas)
        {
            IdColaborador = colaborador.Id;
            IdContrato = idContrato;
            Nome = colaborador.Nome.ToString();
            DataInicio = dataInicio;
            DiasDeFerias = diasDeFerias;
            DiasDeAbono = diasDeAbono;
            PeriodoAquisitivo = periodoAquisitivo;
            FeriasParceladas = feriasParceladas;
        }

        public static AgendamentoDeFeriasQueryEvent Crie(
            Colaborador colaborador,
            Guid idContrato,
            OcorrenciaDeFerias ocorrenciaDeFerias)
            => new AgendamentoDeFeriasQueryEvent(colaborador,
                idContrato,
                ocorrenciaDeFerias.DataDeInicio,
                ocorrenciaDeFerias.DiasDeFerias,
                ocorrenciaDeFerias.DiasDeAbono,
                ocorrenciaDeFerias.PeriodoArquisitivo,
                ocorrenciaDeFerias.FeriasParcelada);
    }
}