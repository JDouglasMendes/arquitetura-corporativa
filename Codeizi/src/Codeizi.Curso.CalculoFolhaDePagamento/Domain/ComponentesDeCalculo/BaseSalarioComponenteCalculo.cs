using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public class BaseSalarioComponenteCalculo : IComponenteDeCalculo
    {
        public EnumComponentesCalculo IdComponente => EnumComponentesCalculo.BaseSalario;

        public ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela)
            => contrato.SalarioContratual;
    }
}