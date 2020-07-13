using BenchmarkDotNet.Attributes;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Benchmark.Test
{
    [MemoryDiagnoser]
    public class CalculoMensalBenchmark
    {        
        public Contrato Execucoes;
        public CalculoFolhaMensal calculo = new CalculoFolhaMensal();

        [GlobalSetup]
        public void Setup()
        {
            Execucoes = new Contrato(Guid.NewGuid(), 
                                    new Vigencia(DateTime.Now, DateTime.Now.AddMonths(1)), 
                                    (ValorComponenteCalculo)1000);            
        }

        [Benchmark]
        public void ExecuteTeste()
            => calculo.Calcule(Execucoes); 

    }
}
