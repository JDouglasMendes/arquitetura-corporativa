using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Infra.CrossCutting.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CodeiziConfiguration : ICodeiziConfiguration
    {
        public IConfiguration Configuration { get; }

        public string ConnectionStringEventSource => Configuration.GetSection("MongoDB:ConnectionString").Value;

        public string ConnectionStringQueryDatabase => Configuration.GetSection("MongoDB:ConnectionString").Value;

        public string DatabaseEventSource => Configuration.GetSection("MongoDB:Database").Value;

        public string ConnectionStringRedis => Configuration.GetSection("Redis:Connection").Value;

        public string CalculoFolhaDePagamentoQueue => Configuration["CalculoFolhaDePagamentoQueue"];

        public string RHQueryQueue => Configuration["RHQueryQueue"];

        public string SignalHbQueue => Configuration["SignalHbQueue"];

        public string AgendamentoDeFeriasQueryBus => Configuration["AgendamentoDeFeriasQueryBus"];

        public string ContratoQueryBus => Configuration["ContratoQueryBus"];

        public string NotificarUsuarioRoutingKey => Configuration["NotificarUsuarioRoutingKey"];

        public CodeiziConfiguration(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
    }
}