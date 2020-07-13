using BenchmarkDotNet.Running;
using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Benchmark.Test
{
    class Program
    {
#pragma warning disable IDE0060 // Remove unused parameter
        static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            BenchmarkRunner.Run<CalculoMensalBenchmark>();            
        }
    }
}
