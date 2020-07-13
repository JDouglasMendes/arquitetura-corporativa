using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    internal static class TabelaDescontoIRRF
    {
        private static readonly List<ValueTuple<ValorComponenteCalculo, ValorComponenteCalculo, double>> _faixas
            = new List<ValueTuple<ValorComponenteCalculo, ValorComponenteCalculo, double>>()
            {
                 (ValorComponenteCalculo.Zero, new ValorComponenteCalculo(1903.98), 0),
                 (new ValorComponenteCalculo(1903.99), new ValorComponenteCalculo(2826.65), 7.5),
                 (new ValorComponenteCalculo(2826.66), new ValorComponenteCalculo(3751.05), 15),
                 (new ValorComponenteCalculo(3751.06), new ValorComponenteCalculo(4664.68), 22.5),
                 (new ValorComponenteCalculo(4664.69), new ValorComponenteCalculo(double.MaxValue), 27.5),
            };

        public static ValorComponenteCalculo CalculeDescontoIRRF(ValorComponenteCalculo salario)
        {
            var faixa = _faixas.FirstOrDefault(x => x.Item1 <= salario && x.Item2 >= salario);
            if (faixa.Item3 == default)
                return ValorComponenteCalculo.Zero;

            return (ValorComponenteCalculo)((salario.Valor * faixa.Item3) / 100);
        }
    }
}
