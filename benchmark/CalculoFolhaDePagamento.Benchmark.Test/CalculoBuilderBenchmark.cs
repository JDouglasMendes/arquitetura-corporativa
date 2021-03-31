using BenchmarkDotNet.Attributes;
using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using CalculoFolhaDePagamento.Domain.Services.Repositories;
using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using System;
using System.Collections.Generic;

namespace CalculoFolhaDePagamento.Benchmark.Test
{
    [MemoryDiagnoser]
    public class CalculoBuilderBenchmark
    {
        public List<Contrato> Execucoes;
        public CalculoBuilder calculo;
        public ICalculoRepository repository = new CalculoMensalMockBanco();
        public IFeedbackExecucaoCalculoServiceDomain feedback = new FeedbackCalculoMock();

        [GlobalSetup]
        public void Setup()
        {
            calculo = new CalculoBuilder(DateTime.Now, EnumFolhaDePagamento.Mensal, repository, feedback);
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
        public async System.Threading.Tasks.Task CalculeContratosParallelForAsync()
            => await calculo.CalculeContratos();

        /*
        [Benchmark]
        public void CalculeContratosThread()
            => calculo.CalculeContratosThread();
        */
    }
}