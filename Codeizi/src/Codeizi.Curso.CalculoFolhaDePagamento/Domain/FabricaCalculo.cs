using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    internal class FabricaCalculo
    {
        internal static ICalculo Crie(EnumFolhaDePagamento enumFolhaDePagamento)
        {
            var calculo = enumFolhaDePagamento switch
            {
                EnumFolhaDePagamento.Mensal => new CalculoFolhaMensal(),
                EnumFolhaDePagamento.Ferias => throw new NotImplementedException(),
                _ => throw new ArgumentException(),
            };

            return calculo;
        }
    }
}