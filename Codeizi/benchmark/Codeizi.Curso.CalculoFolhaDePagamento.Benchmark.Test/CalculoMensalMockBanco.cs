using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Benchmark.Test
{
    public class CalculoMensalMockBanco : ICalculoRepository
    {
        public Task InsiraValoresCalculados(ComponentesCalculados componentesCalculados)
            => Task.CompletedTask;
    }
}