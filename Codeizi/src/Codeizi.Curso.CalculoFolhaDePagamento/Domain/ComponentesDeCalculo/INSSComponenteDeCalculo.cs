using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class INSSComponenteDeCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.Inss;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => TabelaDescontoSalarioContribuicaoINSS.CalculeDescontoINSS(tabela.Valor(EnumComponentesCalculo.BaseSalario));
    }
}