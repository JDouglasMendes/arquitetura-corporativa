using RH.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Integration.Test
{
    public sealed class InMemoryDBContext
    {
        private const string InMemoryConnectionString = "IntegrationTest";

        public static CodeiziContext Crie()
        {
            var options = new DbContextOptionsBuilder<CodeiziContext>()
                .UseInMemoryDatabase(databaseName: InMemoryConnectionString)
                .Options;
            var context = new CodeiziContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}