using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using System.Threading.Tasks;

namespace CalculoFolhaDePagamento.Domain.Services.Repositories
{
    public interface ICalculoRepository
    {
        Task InsiraValoresCalculados(ComponentesCalculados componentesCalculados);
    }
}