using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class IRRFComponenteDeCalculo : IComponenteDeCalculo
    {
        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => TabelaDescontoIRRF.CalculeDescontoIRRF(contrato.SalarioContratual);
    }
}
