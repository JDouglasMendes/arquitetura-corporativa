using RH.Domain.Ocorrencias.Ferias.Validations;
using System;

namespace RH.Domain.Ocorrencias.Ferias.Commands
{
    public class RegistrarOcorrenciaDeFeriasCommand : OcorrenciaDeFeriasCommand
    {
        private readonly RegistrarOcorrenciaDeFeriasCommandValidation _validations;

        public RegistrarOcorrenciaDeFeriasCommand(RegistrarOcorrenciaDeFeriasCommandValidation validations,
            Guid idColaborador,
            Guid idContrato,
            DateTime periodoAquisitivo,
            DateTime dataDeInicio,
            byte diasDeFerias,
            byte diasDeAbono)
        {
            IdColaborador = idColaborador;
            IdContrato = idContrato;
            PeriodoAquisitivo = periodoAquisitivo;
            DataDeInicio = dataDeInicio;
            DiasDeFerias = diasDeFerias;
            DiasDeAbono = diasDeAbono;
            _validations = validations;
        }

        public override bool IsValid()
        {
            ValidationResult = _validations.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}