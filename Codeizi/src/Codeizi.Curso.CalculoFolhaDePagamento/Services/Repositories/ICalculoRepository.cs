using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories
{
    public interface ICalculoRepository
    {
        Task InsiraValoresCalculados(ComponentesCalculados componentesCalculados);
    }
}