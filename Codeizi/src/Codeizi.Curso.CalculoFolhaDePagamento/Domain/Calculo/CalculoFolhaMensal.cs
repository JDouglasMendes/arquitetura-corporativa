using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public class CalculoFolhaMensal : BaseCalculo, ICalculo
    {
        private readonly DateTime _referencia;

        public CalculoFolhaMensal(DateTime referencia)
        {
            _referencia = referencia;
            AdicioneComponenteCalculo<BaseSalarioComponenteCalculo>();
            AdicioneComponenteCalculo<FGTSComponenteCalculo>();
            AdicioneComponenteCalculo<INSSComponenteDeCalculo>();
            AdicioneComponenteCalculo<IRRFComponenteDeCalculo>();
            AdicioneComponenteCalculo<SalarioLiquidoComponenteDeCalculo>();
        }

        public ComponentesCalculados Calcule(Contrato contrato)
            => Calcule(contrato, _referencia);
    }
}