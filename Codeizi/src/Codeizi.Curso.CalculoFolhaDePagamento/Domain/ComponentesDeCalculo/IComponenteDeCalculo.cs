using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.ComponentesDeCalculo
{
    public interface IComponenteDeCalculo
    {
        ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela);

        EnumComponentesCalculo IdComponente { get; }
    }
}