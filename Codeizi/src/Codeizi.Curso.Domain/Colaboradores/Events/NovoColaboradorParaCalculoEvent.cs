﻿using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using System;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Events
{
    public class NovoColaboradorParaCalculoEvent : Event
    {
        public NovoColaboradorParaCalculoEvent(Guid idColaborador, Guid idContrato, DateTime dataInicio, DateTime? dataFim, double salarioContratual)
        {
            AggregateId = idColaborador.ToString();
            IdColaborador = idColaborador;
            IdContrato = idContrato;
            DataInicio = dataInicio;
            DataFim = dataFim;
            SalarioContratual = salarioContratual;
        }

        public Guid IdColaborador { get; private set; }
        public Guid IdContrato { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }
        public double SalarioContratual { get; private set; }
    }
}