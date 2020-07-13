using System;
using System.Collections.Generic;
using System.Text;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public interface IComponenteDeCalculo
    {
        ValorComponenteCalculo Calcule(Contrato contrato, ComponentesCalculados tabela);
    }
}
