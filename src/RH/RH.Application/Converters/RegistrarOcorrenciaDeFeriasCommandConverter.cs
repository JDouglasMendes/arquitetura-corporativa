using AutoMapper;
using RH.Application.ViewModels;
using RH.Domain.Ocorrencias.Ferias.Commands;
using RH.Domain.Ocorrencias.Ferias.Validations;

namespace RH.Application.Converters
{
    public class RegistrarOcorrenciaDeFeriasCommandConverter : ITypeConverter<RegistrarOcorrenciaDeFeriasViewModel, RegistrarOcorrenciaDeFeriasCommand>
    {
        private readonly RegistrarOcorrenciaDeFeriasCommandValidation _registrarOcorrenciaDeFeriasCommandValidation;

        public RegistrarOcorrenciaDeFeriasCommandConverter(RegistrarOcorrenciaDeFeriasCommandValidation registrarOcorrenciaDeFeriasCommandValidation)
        {
            _registrarOcorrenciaDeFeriasCommandValidation = registrarOcorrenciaDeFeriasCommandValidation;
        }

        public RegistrarOcorrenciaDeFeriasCommand Convert(RegistrarOcorrenciaDeFeriasViewModel source,
                                                           RegistrarOcorrenciaDeFeriasCommand destination,
                                                           ResolutionContext context)
        {
            return new RegistrarOcorrenciaDeFeriasCommand(_registrarOcorrenciaDeFeriasCommandValidation,
                source.IdColaborador,
                source.IdContrato,
                source.PeriodoAquisitivo,
                source.DataDeInicio,
                source.DiasDeFerias,
                source.DiasDeAbono);
        }
    }
}