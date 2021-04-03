using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

        private readonly Dictionary<string, string> queuesName = new Dictionary<string, string>();

        public string GetQueue(string name)
        {
            LoadQueues();
            if (queuesName.ContainsKey(name))
                return queuesName[name];

            return string.Empty;
        }

        private void LoadQueues()
        {
            if (queuesName.Any())
                return;

            var queues = Configuration["QueuesName"];

            if (string.IsNullOrEmpty(queues))
                return;

            Array.ForEach(queues.Split('|'), (item) =>
            {
                var keyValue = item.Split(':');

                if (keyValue.Length == 2)
                    queuesName.Add(keyValue[0], keyValue[1]);
            });
        }
    }
}