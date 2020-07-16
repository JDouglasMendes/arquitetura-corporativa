using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public interface ICalculo
    {
        ComponentesCalculados Calcule(Contrato contrato);
    }
}