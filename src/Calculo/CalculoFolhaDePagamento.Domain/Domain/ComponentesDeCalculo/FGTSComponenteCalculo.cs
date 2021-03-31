using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class FGTSComponenteCalculo : IComponenteDeCalculo
    {
        private const byte FATORPERCENTUALFGTS = 8;

        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.FGTS;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => new ValorComponenteCalculo((tabela.Valor(EnumComponentesCalculo.BaseSalario).Valor * FATORPERCENTUALFGTS) / 100);
    }
}