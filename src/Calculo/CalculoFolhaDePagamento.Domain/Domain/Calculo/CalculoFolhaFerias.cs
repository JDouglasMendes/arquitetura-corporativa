using CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;

namespace CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public class CalculoFolhaFerias : BaseCalculo, ICalculo
    {
        private readonly DateTime _referencia;

        public CalculoFolhaFerias(DateTime referencia)
        {
            _referencia = referencia;
            AdicioneComponenteCalculo<BaseSalarioComponenteCalculo>();
            AdicioneComponenteCalculo<BaseSalarioFeriasComponenteCalculo>();
            AdicioneComponenteCalculo<FGTSComponenteCalculo>();
            AdicioneComponenteCalculo<INSSComponenteDeCalculo>();
            AdicioneComponenteCalculo<IRRFComponenteDeCalculo>();
            AdicioneComponenteCalculo<SalarioLiquidoComponenteDeCalculo>();
        }

        public ComponentesCalculados Calcule(Contrato contrato)
            => Calcule(contrato, _referencia);
    }
}