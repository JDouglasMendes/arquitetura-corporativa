using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class BaseSalarioFeriasComponenteCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.BaseFerias;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => new ValorComponenteCalculo(tabela.Valor(EnumComponentesCalculo.BaseSalario).Valor);
    }
}