using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class SalarioLiquidoComponenteDeCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.SalarioLiquido;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => formulaSalarioLiquido(tabela.Valor(EnumComponentesCalculo.BaseSalario),
                                     tabela.Valor(EnumComponentesCalculo.Inss),
                                     tabela.Valor(EnumComponentesCalculo.IRRF));

        private readonly Func<ValorComponenteCalculo, ValorComponenteCalculo,
                    ValorComponenteCalculo, ValorComponenteCalculo>
                    formulaSalarioLiquido = (salarioBase, inss, irrf) => salarioBase - (inss + irrf);
    }
}