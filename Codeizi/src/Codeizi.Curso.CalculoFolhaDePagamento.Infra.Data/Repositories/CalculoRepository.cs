using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Domain.Calculo;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Connection;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Tables;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Repositories
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

            foreach(var c in componentesCalculados.Valores.AsList())
            {
                await connection.ExecuteAsync(sql, new
                {
                    componentesCalculados.IdColaborador,
                    componentesCalculados.IdContrato,
                    Referencia = new FieldDatetimeCustom(componentesCalculados.Referencia).ToInt(),
                    IdValor = (int)c.Key,
                    c.Value.Valor
                });
            };            
        }
    }
}