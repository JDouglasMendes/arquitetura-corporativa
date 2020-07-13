using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class SalarioLiquidoComponenteDeCalculo : IComponenteDeCalculo
    {
        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => formulaSalarioLiquido(contrato.SalarioContratual,
                                     tabela.Valor<INSSComponenteDeCalculo>(),
                                     tabela.Valor<IRRFComponenteDeCalculo>());

        private readonly Func<ValorComponenteCalculo, ValorComponenteCalculo,
                    ValorComponenteCalculo, ValorComponenteCalculo>
                    formulaSalarioLiquido = (salarioContratual, inss, irrf) => salarioContratual - (inss + irrf);
    }
}
