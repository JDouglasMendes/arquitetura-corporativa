using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public interface ICalculo
    {
        ComponentesCalculados Calcule(Contrato contrato);
    }
}
