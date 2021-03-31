using CalculoFolhaDePagamento.Domain.Domain.Calculo;
using CalculoFolhaDePagamento.Domain.Domain.Contratos;
using CalculoFolhaDePagamento.Domain.Services.BusModel;
using CalculoFolhaDePagamento.Infra.BackgroundTasks.Configurations;
using CalculoFolhaDePagamento.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace CalculoFolhaDePagamento.Infra.BackgroundTasks.Tasks
{
    // docker run -d --hostname rabbit-local --name testes-rabbitmq -p 6672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=codeizi -e RABBITMQ_DEFAULT_PASS=TestesCodeizi! rabbitmq:3-management-alpine
    public class ContratoParaCalculoRabbitMQ : IDisposable
    {
        private IConfiguration Configuration { get; }
        private const string filaAdmissao = "novo-contrato";
        private bool disposedValue;
        private IModel _channel;
        private IConnection _connection;

        public ContratoParaCalculoRabbitMQ(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuracao();
        }

        private void Configuracao()
        {
            var rabbitMQConfigurations = new RabbitMQConfigurations();
            new ConfigureFromConfigurationOptions<RabbitMQConfigurations>(
                Configuration.GetSection("RabbitMQConfigurations"))
                    .Configure(rabbitMQConfigurations);

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfigurations.HostName,
                Port = rabbitMQConfigurations.Port,
                UserName = rabbitMQConfigurations.UserName,
                Password = rabbitMQConfigurations.Password
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: filaAdmissao,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume(queue: filaAdmissao,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            var contrato = JsonConvert.DeserializeObject<ContratoBusModel>(message);
            var repository = new ContratoRepository(Configuration);
            repository.InsiraNovoContrato(contrato, x =>
            {
                var contrato = new Contrato(x.IdColaborador,
                                             x.IdContrato,
                                             new Vigencia(x.DataInicio),
                                             new ValorComponenteCalculo(x.SalarioContratual));

                if (x.DataFim.HasValue)
                    contrato.FinalizeContrato(x.DataFim.Value);

                return contrato;
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _channel.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}