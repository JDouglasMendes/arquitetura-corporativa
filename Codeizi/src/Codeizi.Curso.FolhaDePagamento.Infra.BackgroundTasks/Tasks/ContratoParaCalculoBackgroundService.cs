using Codeizi.Curso.CalculoFolhaDePagamento.Infra.BackgroundTasks.Configurations;
using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.BackgroundTasks.Tasks
{
    public class ContratoParaCalculoBackgroundService : BackgroundService
    {
        private readonly ILogger<ContratoParaCalculoBackgroundService> _logger;
        private readonly BackgroundTaskConfigurations _settings;
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IRabbitMQBus _rabbitMQBus;
#pragma warning restore IDE0052 // Remove unread private members

        public IConfiguration Configuration { get; }

        public ContratoParaCalculoBackgroundService(IOptions<BackgroundTaskConfigurations> settings,
                                                    ILogger<ContratoParaCalculoBackgroundService> logger,
                                                    IConfiguration configuration,
                                                    IRabbitMQBus rabbitMQBus)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _rabbitMQBus = rabbitMQBus;
            Configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Serviço de novos contratos iniciado.");

            stoppingToken.Register(() => _logger.LogDebug("#1 Serviço de novos contratos parado devido solicitação administrativa."));

            while (!stoppingToken.IsCancellationRequested)
                await Task.Delay(_settings.CheckUpdateTime > 0 ? _settings.CheckUpdateTime : 1000,
                                stoppingToken);

            _logger.LogDebug("ContratoParaCalculoService finalizado.");

            await Task.CompletedTask;
        }
    }
}