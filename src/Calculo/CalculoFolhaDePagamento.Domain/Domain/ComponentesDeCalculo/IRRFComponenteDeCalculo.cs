using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class IRRFComponenteDeCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.IRRF;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => TabelaDescontoIRRF.CalculeDescontoIRRF(tabela.Valor(EnumComponentesCalculo.BaseSalario));
    }
}