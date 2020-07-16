using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Tables;
using System;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Converters
{
    public class TableToDomainConverter
    {
        public static Func<ContratoTable, Contrato> TableToContratoPadrao = (table) =>
            new Contrato(table.IdColaborador,
                         table.IdContrato,
                         table.DataFim != null ?
                            new Vigencia(new FieldDatetimeCustom(table.DataInicio).ToDateTime().Value, new FieldDatetimeCustom(table.DataFim.Value).ToDateTime().Value) :
                            new Vigencia(new FieldDatetimeCustom(table.DataInicio).ToDateTime().Value),
                         new ValorComponenteCalculo(table.SalarioContratual));
    }
}