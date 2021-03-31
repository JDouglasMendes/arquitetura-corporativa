using RH.Domain.Ocorrencias.Ferias.Commands;
using RH.Domain.Ocorrencias.Ferias.Contracts;
using RH.Domain.Ocorrencias.Ferias.Validations;
using System;

namespace Domain.Test.Ocorrencias
{
    public static class CenarioOcorrenciaDeFeriasCommandBuilder
    {
        public static RegistrarOcorrenciaDeFeriasCommand Crie(RegistrarOcorrenciaDeFeriasCommandValidation validation)
         =>
            new RegistrarOcorrenciaDeFeriasCommand(validation,
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.Now.AddYears(-2),
                DateTime.Now,
                20,
                10);
    }
}