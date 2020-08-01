using Codeizi.Curso.RH.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Codeizi.Curso.Api.Integration.Test
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