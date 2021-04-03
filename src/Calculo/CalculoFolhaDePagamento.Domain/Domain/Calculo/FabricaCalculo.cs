using System;

namespace CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public static class FabricaCalculo
    {
        public static ICalculo Crie(
            EnumFolhaDePagamento enumFolhaDePagamento,
            DateTime referencia) => enumFolhaDePagamento switch
            {
                EnumFolhaDePagamento.Mensal => new CalculoFolhaMensal(referencia),
                EnumFolhaDePagamento.Ferias => new CalculoFolhaFerias(referencia),
                _ => throw new ArgumentException(nameof(enumFolhaDePagamento)),
            };
    }
}