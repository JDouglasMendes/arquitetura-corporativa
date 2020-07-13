using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public class Contrato
    {
        public Contrato(Guid idColaborador, Vigencia vigencia, ValorComponenteCalculo salarioContratual)
        {
            IdColaborador = idColaborador;
            Vigencia = vigencia;
            SalarioContratual = salarioContratual;
        }

        public Guid IdColaborador { get; }
        public Vigencia Vigencia { get; private set; }
        public ValorComponenteCalculo SalarioContratual { get; }
        public bool Ativo => !Vigencia.Fim.HasValue;

        public void FinalizeContrato(DateTime now)
            => Vigencia = new Vigencia(Vigencia.Inicio, now);
    }
}
