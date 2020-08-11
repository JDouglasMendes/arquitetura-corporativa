using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Validations;
using System;

namespace Codeizi.Curso.Domain.Test.Ocorrencias
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