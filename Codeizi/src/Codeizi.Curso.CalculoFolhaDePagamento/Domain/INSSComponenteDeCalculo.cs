using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class INSSComponenteDeCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.Inss;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => TabelaDescontoSalarioContribuicaoINSS.CalculeDescontoINSS(contrato.SalarioContratual);
    }
}
