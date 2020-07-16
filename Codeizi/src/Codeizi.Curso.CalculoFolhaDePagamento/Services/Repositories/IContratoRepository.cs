using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using System;
using System.Collections.Generic;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories
{
    public interface IContratoRepository
    {
        void InsiraNovoContrato(Contrato contrato);

        void InsiraNovoContrato<T>(T contrato, Func<T, Contrato> convertTo)
            where T : class;

        List<Contrato> ObterContratosVigentes(DateTime referencia);
    }
}