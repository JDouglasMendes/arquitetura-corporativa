using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using System;

namespace Codeizi.Curso.Domain.Test.Ocorrencias
{
    public static class CenarioOcorrenciaDeFeriasCommandBuilder
    {
        public static RegistrarOcorrenciaDeFeriasCommand Crie(IOcorrenciaDeDeriasRepository ocorrenciaDeDeriasRepository)
         =>
            new RegistrarOcorrenciaDeFeriasCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.Now.AddYears(-2),
                DateTime.Now,
                20,
                10,
                ocorrenciaDeDeriasRepository);
    }
}