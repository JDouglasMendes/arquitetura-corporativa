using AutoMapper;
using Codeizi.Curso.RH.Application.ViewModels;
using Codeizi.Curso.RH.Domain.Colaboradores.Commands;

namespace Codeizi.Curso.RH.Application.AutoMapper
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
        }
    }
}