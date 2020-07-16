﻿using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Connection
{
    public static class ConnectionFactory
    {
        public static IDbConnection Get(IConfiguration configuration)
            => new SqlConnection(configuration.GetConnectionString("CodeiziCalculoFolhaPagamento"));
    }
}