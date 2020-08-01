using Codeizi.Curso.RH.Application.ViewModels;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Application.Ocorrencias.Ferias
{
    public interface IOcorrenciaDeFeriasAppService
    {
        Task RegistrarOcorrenciaDeFeriasCommand(RegistrarOcorrenciaDeFeriasViewModel ocorrenciaDeFerias);
    }
}