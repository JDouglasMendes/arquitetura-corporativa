using RH.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Service.Api.Factories
{
    public class CodeiziContextFactory : IDesignTimeDbContextFactory<CodeiziContext>
    {
        public CodeiziContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
             .AddJsonFile("appsettings.json")
             .AddEnvironmentVariables()
             .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CodeiziContext>();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: o => o.MigrationsAssembly("RH.Infra.Data"));

            return new CodeiziContext(optionsBuilder.Options);
        }
    }
}
