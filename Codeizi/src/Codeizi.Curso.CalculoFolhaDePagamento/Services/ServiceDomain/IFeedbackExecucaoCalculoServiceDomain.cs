using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain
{
    public interface IFeedbackExecucaoCalculoServiceDomain
    {
        void IniciarProcessamento(Guid idExecucao, int quantidadeParaProcessamento);
        void AtualizarPercentualExecucao(Guid idExecucao,
                                         int quantidadeProcessada,
                                         int quantidadeTotalProcessamento);

        int PercentualExecucao(Guid idExecucao);
    }
}
