using Codeizi.Curso.RH.Application.ViewModels;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Application.Colaboradores
{
    public interface IColaboradorAppService
    {
        Task RealizeAdmissao(ColaboradorAdmissaoViewModel colaboradorAdmissaoViewModel);
    }
}