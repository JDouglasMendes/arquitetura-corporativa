using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Domain.Colaboradores.Contracts
{
    public interface IColaboradorRepository
    {
        IEnumerable<Contrato> BusqueTodosContratos(Guid idColaborador);
        Task<Contrato> ObtenhaContrato(Guid idContrato);
        Task RealizeAdmissao(Colaborador colaborador);
        Task<Colaborador> BusqueColaborador(Guid idColaborador);
    }
}