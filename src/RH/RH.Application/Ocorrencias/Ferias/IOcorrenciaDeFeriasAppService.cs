using RH.Application.ViewModels;
using System.Threading.Tasks;

namespace RH.Application.Ocorrencias.Ferias
{
    public interface IOcorrenciaDeFeriasAppService
    {
        Task RegistrarOcorrenciaDeFeriasCommand(RegistrarOcorrenciaDeFeriasViewModel ocorrenciaDeFerias);
    }
}