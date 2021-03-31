using RH.Application.ViewModels;
using System.Threading.Tasks;

namespace RH.Application.Colaboradores
{
    public interface IColaboradorAppService
    {
        Task RealizeAdmissao(ColaboradorAdmissaoViewModel colaboradorAdmissaoViewModel);
    }
}