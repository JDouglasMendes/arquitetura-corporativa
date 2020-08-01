using Codeizi.Curso.RH.Domain.Colaboradores;
using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using System;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Events
{
    public class AgendamentoDeFeriasQueryEvent : Event
    {
        public AgendamentoDeFeriasQueryEvent(Guid idColaborador,
            Guid idContrato,
            string nome,
            string sobrenome,
            DateTime dataInicio,
            byte diasDeFerias,
            byte diasDeAbono,
            DateTime periodoAquisitivo,
            bool feriasParceladas)
        {
            IdColaborador = idColaborador;
            IdContrato = idContrato;
            Nome = nome;
            Sobrenome = sobrenome;
            DataInicio = dataInicio;
            DiasDeFerias = diasDeFerias;
            DiasDeAbono = diasDeAbono;
            PeriodoAquisitivo = periodoAquisitivo;
            FeriasParceladas = feriasParceladas;
        }

        public static AgendamentoDeFeriasQueryEvent Crie(Colaborador colaborador, Guid idContrato, OcorrenciaDeFerias ocorrenciaDeFerias)
            => new AgendamentoDeFeriasQueryEvent(colaborador.Id,
                idContrato,
                colaborador.Nome.Nome,
                colaborador.Nome.Sobrenome,
                ocorrenciaDeFerias.DataDeInicio,
                ocorrenciaDeFerias.DiasDeFerias,
                ocorrenciaDeFerias.DiasDeAbono,
                ocorrenciaDeFerias.PeriodoArquisitivo,
                ocorrenciaDeFerias.FeriasParcelada);

        public Guid IdColaborador { get; }
        public Guid IdContrato { get; }
        public string Nome { get; set; }
        public string Sobrenome { get; }
        public DateTime DataInicio { get; }
        public byte DiasDeFerias { get; }
        public byte DiasDeAbono { get; }
        public DateTime PeriodoAquisitivo { get; }
        public bool FeriasParceladas { get; }

        public DateTime DataFim => DataInicio.AddDays(DiasDeFerias);
    }
}