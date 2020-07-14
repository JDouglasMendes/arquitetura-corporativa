using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class FGTSComponenteCalculo : IComponenteDeCalculo
    {
        private const byte FATORPERCENTUALFGTS = 8;

        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.FGTS;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => new ValorComponenteCalculo((contrato.SalarioContratual.Valor * FATORPERCENTUALFGTS) / 100);
    }
}
