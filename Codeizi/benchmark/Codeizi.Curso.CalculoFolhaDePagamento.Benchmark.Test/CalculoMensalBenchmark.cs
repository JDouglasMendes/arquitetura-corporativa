using BenchmarkDotNet.Attributes;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Benchmark.Test
{
    [MemoryDiagnoser]
    public class CalculoMensalBenchmark
    {
        public Contrato Execucoes;
        public CalculoFolhaMensal calculo = new CalculoFolhaMensal(DateTime.Now);

        [GlobalSetup]
        public void Setup()
        {
            Execucoes = new Contrato(Guid.NewGuid(),
                                    Guid.NewGuid(),
                                    new Vigencia(DateTime.Now, DateTime.Now.AddMonths(1)),
                                    (ValorComponenteCalculo)1000);
        }

        [Benchmark]
        public void ExecuteTeste()
            => calculo.Calcule(Execucoes);
    }
}