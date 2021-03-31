using RH.Infra.Data.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace Codeizi.Infra.Data.Test.Repository
{
    public sealed class InMemoryDBContext : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        public InMemoryDBContext()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
        }

        public CodeiziContext Crie()
        {
            var options = new DbContextOptionsBuilder<CodeiziContext>()
                .UseSqlite(_connection)
                .Options;
            var context = new CodeiziContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        public void Dispose()
            => _connection.Close();
    }
}