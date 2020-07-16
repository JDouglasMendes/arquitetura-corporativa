using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codeizi.Curso.Infra.CrossCutting.Configuration
{
    public class CodeiziConfiguration : ICodeiziConfiguration
    {
        public IConfiguration Configuration { get; }

        public string ConnectionStringRavenDB => Configuration.GetSection("RavenDB:ConnectionString").Value;

        public string DatabaseRavenDB => Configuration.GetSection("RavenDB:Database").Value;

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