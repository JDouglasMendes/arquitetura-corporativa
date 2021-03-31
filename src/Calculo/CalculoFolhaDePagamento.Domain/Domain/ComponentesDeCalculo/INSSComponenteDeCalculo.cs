using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class INSSComponenteDeCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.Inss;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => TabelaDescontoSalarioContribuicaoINSS.CalculeDescontoINSS(tabela.Valor(EnumComponentesCalculo.BaseSalario));
    }
}