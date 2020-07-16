using AutoMapper;
using Codeizi.Curso.Application.ViewModels;
using Codeizi.Curso.Domain.Colaboradores.Commands;

namespace Codeizi.Curso.Application.AutoMapper
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