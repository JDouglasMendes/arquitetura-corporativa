using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class IRRFComponenteDeCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.IRRF;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => TabelaDescontoIRRF.CalculeDescontoIRRF(tabela.Valor(EnumComponentesCalculo.BaseSalario));
    }
}