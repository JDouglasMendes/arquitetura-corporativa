using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class SalarioLiquidoComponenteDeCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.SalarioLiquido;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => formulaSalarioLiquido(contrato.SalarioContratual,
                                     tabela.Valor(EnumComponentesCalculo.Inss),
                                     tabela.Valor(EnumComponentesCalculo.IRRF));

        private readonly Func<ValorComponenteCalculo, ValorComponenteCalculo,
                    ValorComponenteCalculo, ValorComponenteCalculo>
                    formulaSalarioLiquido = (salarioContratual, inss, irrf) => salarioContratual - (inss + irrf);
    }
}
