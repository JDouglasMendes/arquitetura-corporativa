using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain
{
    public interface ICalculoRepository
    {
        Task InsiraValoresCalculadosAsync(ComponentesCalculados componentesCalculados);
    }
}
