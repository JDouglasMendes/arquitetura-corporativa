using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infra.CrossCutting.EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IRabbitMQBus, IDisposable
    {
        private const string BROKER_NAME = "codeizi_event_bus";

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly int _retryCount;
        private readonly IServiceCollection _services;

        private IModel _consumerChannel;
        private bool disposedValue;
        private readonly string _queueName;
        private readonly Type _type;
        private Dictionary<string, IConsumerServiceBus> _consumers;

        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection,
                                ILogger<EventBusRabbitMQ> logger,
                                IServiceCollection services,
                                Type type,
                                string queueName = null,
                                int retryCount = 5)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
            _retryCount = retryCount;
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogTrace("Creating RabbitMQ consumer channel");

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME,
                                    type: "direct");

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };
            return channel;
        }

        private void StartBasicConsume()
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private static readonly object _lock = new object();

        private void LoadConsumers()
        {
            lock (_lock)
            {
                if (_consumers != null)
                    return;

                _consumers = new Dictionary<string, IConsumerServiceBus>();

                var consumersType = _type.Assembly.GetTypes().Where(t =>
                                        {
                                            if (t.IsClass && !t.IsAbstract && t.GetInterface(nameof(IConsumerServiceBus)) != null)
                                                return true;
                                            return false;
                                        }).ToList();

                consumersType.ForEach(type =>
                {
                    var instance = _services.BuildServiceProvider().GetRequiredService(type);
                    if (instance is IConsumerServiceBus consumer)
                        _consumers.Add(consumer.RoutingKey, consumer);
                });
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            LoadConsumers();

            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
            }

            // Even on exception we take the message off the queue.
            // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX).
            // For more information see: https://www.rabbitmq.com/dlx.html
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);
            /*
            var handle = _services
                .BuildServiceProvider()
                .GetRequiredService(GetTypeByName(eventName));

            var publishable = JsonConvert.DeserializeObject<Publishable>(message);

            await (Task)handle.GetType().GetMethod("Handle").Invoke(handle, new object[] { publishable });
            */
            var publishable = JsonConvert.DeserializeObject<Publishable>(message);
            if (_consumers.ContainsKey(eventName))
                await _consumers[eventName].Handle(publishable);
        }
        /*
        private Type GetTypeByName(string eventName)
        {
            var result = _type.Assembly.GetTypes().Where(t =>
            {
                if (t.GetCustomAttribute(typeof(ServiceMediatorBusAttribute)) != null)
                    if ((t.GetCustomAttribute(typeof(ServiceMediatorBusAttribute)) as ServiceMediatorBusAttribute).Queue == eventName)
                        return true;
                return false;
            }).FirstOrDefault();

            return result;
        }
        */
        public Task Publisher<T>(T publishable) where T : Publishable
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", publishable.Id, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var eventName = publishable.EventName;

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", publishable.Id, eventName);

            using (var channel = _persistentConnection.CreateModel())
            {
                _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", publishable.Id);

                channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

                var message = JsonConvert.SerializeObject(publishable);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", publishable.Id);

                    channel.BasicPublish(
                        exchange: BROKER_NAME,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });
            }

            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_consumerChannel != null)
                    {
                        _consumerChannel.Dispose();
                    }
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