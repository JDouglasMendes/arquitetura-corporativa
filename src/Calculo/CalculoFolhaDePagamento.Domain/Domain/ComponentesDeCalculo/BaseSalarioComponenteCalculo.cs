using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class BaseSalarioComponenteCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.BaseSalario;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => contrato.SalarioContratual;
    }
}