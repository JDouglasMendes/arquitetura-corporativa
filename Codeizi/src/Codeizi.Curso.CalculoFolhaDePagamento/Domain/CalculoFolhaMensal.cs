using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class CalculoFolhaMensal : BaseCalculo
    {
        public CalculoFolhaMensal()
        {
            AdicioneComponenteCalculo<FGTSComponenteCalculo>();
            AdicioneComponenteCalculo<INSSComponenteDeCalculo>();
            AdicioneComponenteCalculo<IRRFComponenteDeCalculo>();
            AdicioneComponenteCalculo<SalarioLiquidoComponenteDeCalculo>();
        }

        public ComponentesCalculados Calcule(Contrato contrato)
        {
            var tabela = new ComponentesCalculados();

            foreach (var componente in Componentes)
                tabela.AdicioneValor(componente, componente.Calcule(contrato, tabela));

            return tabela;
        }
    }
}
