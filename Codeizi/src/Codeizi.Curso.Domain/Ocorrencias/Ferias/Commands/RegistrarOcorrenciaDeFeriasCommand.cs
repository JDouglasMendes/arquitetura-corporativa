using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Validations;
using System;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands
{
    public class RegistrarOcorrenciaDeFeriasCommand : OcorrenciaDeFeriasCommand
    {
        private readonly IOcorrenciaDeDeriasRepository _ocorrenciaDeDeriasRepository;
        public RegistrarOcorrenciaDeFeriasCommand(Guid idContrato,
            DateTime periodoAquisitivo,
            DateTime dataDeInicio,
            byte diasDeFerias,
            byte diasDeAbono,
            IOcorrenciaDeDeriasRepository ocorrenciaDeDeriasRepository)
        {
            IdContrato = idContrato;
            PeriodoAquisitivo = periodoAquisitivo;
            DataDeInicio = dataDeInicio;
            DiasDeFerias = diasDeFerias;
            DiasDeAbono = diasDeAbono;
            _ocorrenciaDeDeriasRepository = ocorrenciaDeDeriasRepository;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarOcorrenciaDeFeriasCommandValidation(_ocorrenciaDeDeriasRepository)
                .Validate(this);

            return ValidationResult.IsValid;
        }

    }
}