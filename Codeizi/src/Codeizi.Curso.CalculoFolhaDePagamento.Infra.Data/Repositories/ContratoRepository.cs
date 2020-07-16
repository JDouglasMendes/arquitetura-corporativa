using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Contratos;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Connection;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Converters;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Tables;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Repositories
{
    public class ContratoRepository : IContratoRepository
    {
        private IConfiguration Configuration { get; }

        public ContratoRepository(IConfiguration configuration)
            => Configuration = configuration;

        public void InsiraNovoContrato(Contrato contrato)
        {
            using var connection = ConnectionFactory.Get(Configuration);

            var sql = @"INSERT INTO CONTRATO (IdColaborador,IdContrato,DataInicio,DataFim,SalarioContratual)" +
                        " VALUES (@IdColaborador,@IdContrato,@DataInicio,@DataFim,@SalarioContratual)";

            connection.Execute(sql, new
            {
                contrato.IdColaborador,
                contrato.IdContrato,
                DataInicio = new FieldDatetimeCustom(contrato.Vigencia.Inicio).ToInt(),
                DataFim = new FieldDatetimeCustom(contrato.Vigencia.Fim).ToInt(),
                SalarioContratual = contrato.SalarioContratual.Valor
            });
        }

        public void InsiraNovoContrato<T>(T contrato, Func<T, Contrato> convertTo)
            where T : class
            => InsiraNovoContrato(convertTo(contrato));

        public List<Contrato> ObterContratosVigentes(DateTime referencia)
        {
            var parametros = new
            {
                inicio = new FieldDatetimeCustom(referencia).ToInt(),
                fim = new FieldDatetimeCustom(referencia).ToInt()
            };

            var sql = "SELECT Id,IdColaborador,IdContrato,DataInicio,DataFim,SalarioContratual " +
                       " from CONTRATO " +
                       " WHERE DataInicio BETWEEN @inicio AND @fim";

            using var connection = ConnectionFactory.Get(Configuration);
            var listas = connection.Query<ContratoTable>(sql, parametros);
            return listas
                .ToList()
                .ConvertAll(new Converter<ContratoTable, Contrato>(TableToDomainConverter.TableToContratoPadrao));
        }
    }
}