using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using Codeizi.Curso.Infra.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Services
{
    public class FeedbackExecucaoCalculoServiceDomain : IFeedbackExecucaoCalculoServiceDomain
    {
        private readonly DatabaseRedis databaseRedis;

        public FeedbackExecucaoCalculoServiceDomain(DatabaseRedis databaseRedis)
            => this.databaseRedis = databaseRedis;
        
        public void IniciarProcessamento(Guid idExecucao, int quantidadeParaProcessamento)
        {
            var client = databaseRedis.GetClient();
            client.StringSet(idExecucao.ToString(), CamposProcessamentoCalculo(quantidadeParaProcessamento));
        }

        private static string CamposProcessamentoCalculo(int quantidadeParaProcessamento)
            => string.Concat(DateTime.Now.ToString(), "|", quantidadeParaProcessamento);

        public void AtualizarPercentualExecucao(Guid idExecucao, int quantidadeProcessada, int quantidadeTotalProcessamento)
        {
            if (EhMultiplo(quantidadeProcessada * 100 / quantidadeTotalProcessamento, 10))
            {
                // Mensagem SignalIR
            }
        }

        private static bool EhMultiplo(double valor, int fator)
            => valor % fator == 0;

        private enum EnumPosicaoValores
        {
            DataInicioProcessamento,
            QuantidadeProcessada
        }
    }
}
