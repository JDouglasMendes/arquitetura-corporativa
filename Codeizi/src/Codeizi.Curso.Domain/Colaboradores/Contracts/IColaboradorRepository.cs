using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codeizi.Curso.Domain.Colaboradores.Contracts
{
    public interface IColaboradorRepository
    {
        IEnumerable<Contrato> BusqueTodosContratos(Guid guid);
        Task RealizeAdmissao(Colaborador colaborador);
    }
}
