using RH.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RH.Service.Api.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            if (!string.IsNullOrEmpty(configuration.GetConnectionString("DefaultConnection")))
            {
                services.AddDbContext<CodeiziContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("RH.Infra.Data")));
            }
            else
            {
                _ = services.AddDbContext<CodeiziContext>(options =>
                         options.UseInMemoryDatabase(databaseName: "IntegrationTest"));
            }

            return services;
        }
    }
}