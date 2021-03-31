using CalculoFolhaDePagamento.Domain.Domain.Contratos;

namespace CalculoFolhaDePagamento.Domain.Domain.Calculo
{
    public interface ICalculo
    {
        ComponentesCalculados Calcule(Contrato contrato);
    }
}