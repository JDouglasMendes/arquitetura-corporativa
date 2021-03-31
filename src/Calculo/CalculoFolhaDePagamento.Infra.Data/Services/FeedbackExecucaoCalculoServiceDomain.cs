using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using Infra.CrossCutting.Redis;
using System;

namespace CalculoFolhaDePagamento.Infra.Data.Services
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

        private static string CamposProcessamentoCalculo(string dataProcessamento, double quantidadeParaProcessamento)
            => string.Concat(dataProcessamento, "|", quantidadeParaProcessamento);

        private static string Conteudo(string conteudo, EnumPosicaoValores enumPosicaoValores)
            => conteudo?.Split('|')?[(int)enumPosicaoValores];

        private static bool EhMultiplo(double valor, int fator)
            => valor % fator == 0;

        private void AtualizaStatusProcessamento(Guid idExecucao, double percentual)
        {
            var conteudo = databaseRedis.GetClient().StringGet(idExecucao.ToString());
            databaseRedis.GetClient().StringSet(idExecucao.ToString(), CamposProcessamentoCalculo(Conteudo(conteudo, EnumPosicaoValores.DataInicioProcessamento),
                                                                                                    percentual));
        }

        public void AtualizarPercentualExecucao(Guid idExecucao, int quantidadeProcessada, int quantidadeTotalProcessamento)
        {
            if (EhMultiplo(quantidadeProcessada * 100 / quantidadeTotalProcessamento, 10))
            {
                AtualizaStatusProcessamento(idExecucao, quantidadeProcessada * 100 / quantidadeTotalProcessamento);
            }
        }

        public int PercentualExecucao(Guid idExecucao)
        {
            var conteudo = databaseRedis.GetClient().StringGet(idExecucao.ToString());
            if (conteudo.HasValue)
                return int.Parse(Conteudo(conteudo, EnumPosicaoValores.QuantidadeProcessada));

            return 0;
        }

        private enum EnumPosicaoValores
        {
            DataInicioProcessamento,
            QuantidadeProcessada
        }
    }
}