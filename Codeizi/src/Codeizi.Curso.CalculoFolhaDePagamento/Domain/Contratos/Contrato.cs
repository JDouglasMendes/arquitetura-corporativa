using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos
{
    public class Contrato
    {
        public Contrato(Guid idColaborador, Guid idContrato, Vigencia vigencia, ValorComponenteCalculo salarioContratual)
        {
            IdColaborador = idColaborador;
            Vigencia = vigencia;
            SalarioContratual = salarioContratual;
            IdContrato = idContrato;
        }

        public Guid IdColaborador { get; }
        public Guid IdContrato { get; }
        public Vigencia Vigencia { get; private set; }
        public ValorComponenteCalculo SalarioContratual { get; }
        public bool Ativo => !Vigencia.Fim.HasValue;

        public void FinalizeContrato(DateTime now)
            => Vigencia = new Vigencia(Vigencia.Inicio, now);
    }
}