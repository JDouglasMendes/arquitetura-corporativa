﻿using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using CalculoFolhaDePagamento.Infra.Data.Tables;
using System;

namespace CalculoFolhaDePagamento.Infra.Data.Converters
{
    public static class TableToDomainConverter
    {
        public readonly static Func<ContratoTable, Contrato> Contrato = (table) =>
            new Contrato(Guid.Parse(table.IdColaborador),
                         Guid.Parse(table.IdContrato),
                         table.DataFim != null && table.DataFim.Value > 0 ?
                            new Vigencia(new FieldDatetimeCustom(table.DataInicio).ToDateTime().Value, new FieldDatetimeCustom(table.DataFim.Value).Value.Value) :
                            new Vigencia(new FieldDatetimeCustom(table.DataInicio).ToDateTime().Value),
                         new ValorComponenteCalculo(table.SalarioContratual));
    }
}