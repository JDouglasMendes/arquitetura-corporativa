using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public interface IComponenteDeCalculo
    {
        ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela);

        EnumComponentesCalculo IdComponente { get; }
    }
}