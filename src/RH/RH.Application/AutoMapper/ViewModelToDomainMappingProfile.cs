using AutoMapper;
using RH.Application.Converters;
using RH.Application.ViewModels;
using RH.Domain.Colaboradores.Commands;
using RH.Domain.Ocorrencias.Ferias.Commands;

namespace RH.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ColaboradorAdmissaoViewModel, AdmissaoColaboradorCommand>()
                .ConvertUsing(x => new AdmissaoColaboradorCommand(x.Nome, x.Sobrenome, x.DataDeAdmissao, x.SalarioContratual, x.DataNascimento)
                {
                    ObservacaoContratual = x.ObservacaoContratual,
                });

            CreateMap<RegistrarOcorrenciaDeFeriasViewModel, RegistrarOcorrenciaDeFeriasCommand>()
                .ConvertUsing<RegistrarOcorrenciaDeFeriasCommandConverter>();
        }
    }
}