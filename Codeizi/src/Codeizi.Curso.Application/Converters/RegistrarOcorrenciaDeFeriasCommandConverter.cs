using AutoMapper;
using Codeizi.Curso.RH.Application.ViewModels;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Commands;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.RH.Application.Converters
{
    public class RegistrarOcorrenciaDeFeriasCommandConverter : ITypeConverter<RegistrarOcorrenciaDeFeriasViewModel, RegistrarOcorrenciaDeFeriasCommand>
    {
        private readonly IOcorrenciaDeDeriasRepository _ocorrenciaDeDeriasRepository;

        public RegistrarOcorrenciaDeFeriasCommandConverter(IOcorrenciaDeDeriasRepository ocorrenciaDeDeriasRepository)
        {
            _ocorrenciaDeDeriasRepository = ocorrenciaDeDeriasRepository;
        }

        public RegistrarOcorrenciaDeFeriasCommand Convert(RegistrarOcorrenciaDeFeriasViewModel source,
                                                           RegistrarOcorrenciaDeFeriasCommand destination,
                                                           ResolutionContext context)
        {
            return new RegistrarOcorrenciaDeFeriasCommand(source.IdColaborador,
                source.IdContrato,
                source.PeriodoAquisitivo,
                source.DataDeInicio,
                source.DiasDeFerias,
                source.DiasDeAbono,
                _ocorrenciaDeDeriasRepository);

        }
    }
}
