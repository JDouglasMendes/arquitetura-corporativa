using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Services.Repositories;
using System.Threading.Tasks;

namespace CalculoFolhaDePagamento.Benchmark.Test
{
    public class CalculoMensalMockBanco : ICalculoRepository
    {
        public Task InsiraValoresCalculados(ComponentesCalculados componentesCalculados)
            => Task.CompletedTask;
    }
}