using Infra.CrossCutting.EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RH.Service.Api.IntegrationFake;

namespace RH.Service.Api.Extensions
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
            => configuration.GetValue<bool>("isTest") ?
            EventBusFake(services) :
            EventBusRabbitMQ(services, configuration);

        private static IServiceCollection EventBusRabbitMQ(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true,
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                    factory.UserName = configuration["EventBusUserName"];

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                    factory.Password = configuration["EventBusPassword"];

                if (!string.IsNullOrEmpty(configuration["EventBusPort"]))
                    factory.Port = int.Parse(configuration["EventBusPort"]);

                var retryCount = 5;

                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IRabbitMQBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();

                return new EventBusRabbitMQ(rabbitMQPersistentConnection,
                                            logger,
                                            services,
                                            typeof(EventBusRabbitMQ),
                                            queueName: "add-contrato");
            });

            return services;
        }

        private static IServiceCollection EventBusFake(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQPersistentConnection, RabbitMQPersistentConnectionFake>();
            services.AddSingleton<IRabbitMQBus, RabbitMQBusFake>();
            return services;
        }
    }
}