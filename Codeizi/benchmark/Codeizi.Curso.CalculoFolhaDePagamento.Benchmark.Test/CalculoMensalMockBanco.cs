using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Benchmark.Test
{
    public class CalculoMensalMockBanco : ICalculoRepository
    {
        public Task InsiraValoresCalculadosAsync(ComponentesCalculados componentesCalculados)
            => Task.CompletedTask;
    }
}
