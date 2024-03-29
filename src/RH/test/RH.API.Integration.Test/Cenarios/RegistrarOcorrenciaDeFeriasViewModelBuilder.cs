﻿using RH.Application.ViewModels;
using RH.Domain.Colaboradores;
using System;

namespace Api.Integration.Test.Cenarios
{
    public static class RegistrarOcorrenciaDeFeriasViewModelBuilder
    {
        public static RegistrarOcorrenciaDeFeriasViewModel Crie(Guid idContrato)
            => new RegistrarOcorrenciaDeFeriasViewModel
            {
                DataDeInicio = DateTime.Now,
                DiasDeAbono = 0,
                DiasDeFerias = 30,
                PeriodoAquisitivo = DateTime.Now.AddYears(-2),
                IdContrato = idContrato,
            };

        public static RegistrarOcorrenciaDeFeriasViewModel CrieConsiderandoContrato(Contrato contrato)
                    => new RegistrarOcorrenciaDeFeriasViewModel
                    {
                        DataDeInicio = DateTime.Now,
                        DiasDeAbono = 0,
                        DiasDeFerias = 30,
                        PeriodoAquisitivo = contrato.PeriodoAquisitivo,
                        IdContrato = contrato.Id,
                        IdColaborador = contrato.ColaboradorId,
                    };
    }
}