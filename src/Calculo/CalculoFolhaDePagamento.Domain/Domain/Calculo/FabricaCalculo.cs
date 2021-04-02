using System;

namespace CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    internal static class FabricaCalculo
    {
        internal static ICalculo Crie(EnumFolhaDePagamento enumFolhaDePagamento, DateTime referencia)
        {
            var calculo = enumFolhaDePagamento switch
            {
                EnumFolhaDePagamento.Mensal => new CalculoFolhaMensal(referencia),
                EnumFolhaDePagamento.Ferias => throw new NotImplementedException(),
                _ => throw new ArgumentException(nameof(enumFolhaDePagamento)),
            };

            return calculo;
        }
    }
}