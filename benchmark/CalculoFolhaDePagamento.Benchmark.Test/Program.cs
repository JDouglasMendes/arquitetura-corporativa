using BenchmarkDotNet.Running;

namespace CalculoFolhaDePagamento.Benchmark.Test
{
    internal class Program
    {
#pragma warning disable IDE0060 // Remove unused parameter

        private static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            BenchmarkRunner.Run<CalculoBuilderBenchmark>();
        }
    }
}