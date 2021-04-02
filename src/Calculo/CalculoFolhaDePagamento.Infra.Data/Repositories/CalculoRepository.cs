using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Services.Repositories;
using CalculoFolhaDePagamento.Infra.Data.Connection;
using CalculoFolhaDePagamento.Infra.Data.Tables;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CalculoFolhaDePagamento.Infra.Data.Repositories
{
    public class CalculoRepository : ICalculoRepository
    {
        private IConfiguration Configuration { get; }

        public CalculoRepository(IConfiguration configuration)
            => Configuration = configuration;

        public async Task InsiraValoresCalculados(ComponentesCalculados componentesCalculados)
        {
            using var connection = ConnectionFactory.Get(Configuration);

            var sql = @"INSERT INTO VALORESCALCULADOS (IdColaborador,IdContrato,Referencia,IdValor,Valor)" +
                        " VALUES (@IdColaborador,@IdContrato,@Referencia,@IdValor,@Valor)";

            foreach (var c in componentesCalculados.Valores.AsList())
            {
                await connection.ExecuteAsync(sql, new
                {
                    componentesCalculados.IdColaborador,
                    componentesCalculados.IdContrato,
                    Referencia = new FieldDatetimeCustom(componentesCalculados.Referencia).ToInt(),
                    IdValor = (int)c.Key,
                    c.Value.Valor
                });
            }
        }
    }
}