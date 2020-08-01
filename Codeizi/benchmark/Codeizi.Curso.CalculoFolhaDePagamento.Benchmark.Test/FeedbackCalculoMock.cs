using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Benchmark.Test
{
    public class FeedbackCalculoMock : IFeedbackExecucaoCalculoServiceDomain
    {
        public void AtualizarPercentualExecucao(Guid idExecucao, int quantidadeProcessada, int quantidadeTotalProcessamento)
        {
        }

        public void IniciarProcessamento(Guid idExecucao, int quantidadeParaProcessamento)
        {
        }

        public int PercentualExecucao(Guid idExecucao)
        {
            return 100;
        }
    }
}