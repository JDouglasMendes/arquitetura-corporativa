using BenchmarkDotNet.Attributes;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using System;
using System.Collections.Generic;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Benchmark.Test
{
    [MemoryDiagnoser]
    public class CalculoBuilderBenchmark
    {
        public List<Contrato> Execucoes;
        public CalculoBuilder calculo;
        public ICalculoRepository repository = new CalculoMensalMockBanco();

        [GlobalSetup]
        public void Setup()
        {
            calculo = new CalculoBuilder(DateTime.Now, EnumFolhaDePagamento.Mensal, repository);
            var quantidade = 1_000_000;
            Execucoes = new List<Contrato>(quantidade);
            var id = Guid.NewGuid();
            var vigencia = new Vigencia(DateTime.Now, DateTime.Now.AddMonths(12));
            var valor = new ValorComponenteCalculo(1000);
            while (quantidade > 0)
            {
                Execucoes.Add(new Contrato(id, Guid.NewGuid(), vigencia, valor));
                quantidade--;
            }
            calculo.InicieCalculo(Execucoes);
        }

        [Benchmark]
        public void CalculeContratosParallelFor()
            => calculo.CalculeContratos();

        /*
        [Benchmark]
        public void CalculeContratosThread()
            => calculo.CalculeContratosThread();
        */
    }
}