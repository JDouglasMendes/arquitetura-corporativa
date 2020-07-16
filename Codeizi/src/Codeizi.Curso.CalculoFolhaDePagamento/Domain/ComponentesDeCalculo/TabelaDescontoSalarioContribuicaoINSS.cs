using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    internal static class TabelaDescontoSalarioContribuicaoINSS
    {
        private static readonly ValorComponenteCalculo TetoDescontoINSS = new ValorComponenteCalculo(854.1484);

        private static readonly List<ValueTuple<ValorComponenteCalculo, ValorComponenteCalculo, double>> _faixas
            = new List<ValueTuple<ValorComponenteCalculo, ValorComponenteCalculo, double>>()
            {
                 (ValorComponenteCalculo.Zero, new ValorComponenteCalculo(1045), 7.5),
                 (new ValorComponenteCalculo(1045.01), new ValorComponenteCalculo(2089.60), 9),
                 (new ValorComponenteCalculo(2089.61), new ValorComponenteCalculo(3131.40), 12),
                 (new ValorComponenteCalculo(3134.41), new ValorComponenteCalculo(6101.06), 14),
            };

        public static ValorComponenteCalculo CalculeDescontoINSS(ValorComponenteCalculo salario)
        {
            var faixa = _faixas.FirstOrDefault(x => x.Item1 <= salario && x.Item2 >= salario);
            if (faixa.Item3 == default)
                return TetoDescontoINSS;

            return (ValorComponenteCalculo)((salario.Valor * faixa.Item3) / 100);
        }
    }
}