using Infra.CrossCutting.Configuration;
using Infra.CrossCutting.EventBusRabbitMQ;
using MediatR;
using RH.Domain.Colaboradores.Events;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Domain.Colaboradores.EventHandlers
{
    public class ColaboradorEventHandler :
        INotificationHandler<NovoColaboradorParaCalculoEvent>,
        INotificationHandler<ColaboradorEventSource>,
        INotificationHandler<ContratoQueryEvent>

    {
        private readonly IRabbitMQBus _rabbitMQBus;
        private readonly ICodeiziConfiguration _configuration;

        public ColaboradorEventHandler(
            IRabbitMQBus rabbitMQBus,
            ICodeiziConfiguration configuration)
        {
            _rabbitMQBus = rabbitMQBus;
            _configuration = configuration;
        }

        public Task Handle(
            NovoColaboradorParaCalculoEvent notification,
            CancellationToken cancellationToken)
        {
            var queue = _configuration.GetQueue(notification.GetKeyQueues);
            if (!string.IsNullOrEmpty(queue))
            {
                _rabbitMQBus.Publisher(FactoryPublishable.Get(notification.AggregateId,
                                                              queue,
                                                              notification));
            }

            return Task.CompletedTask;
        }

        public Task Handle(
            ColaboradorEventSource notification,
            CancellationToken cancellationToken)
        {
            if (!notification.MessageType.Equals("DomainNotification"))
            {
                var queue = _configuration.GetQueue(notification.GetKeyQueues);
                if (!string.IsNullOrEmpty(queue))
                {
                    _rabbitMQBus.Publisher(FactoryPublishable.Get(notification.AggregateId,
                                                                  queue,
                                                                  notification));
                }

                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        public Task Handle(
            ContratoQueryEvent notification,
            CancellationToken cancellationToken)
        {
            var queue = _configuration.GetQueue(notification.GetKeyQueues);
            if (!string.IsNullOrEmpty(queue))
            {
                _rabbitMQBus.Publisher(FactoryPublishable.Get(notification.AggregateId,
                                                              queue,
                                                              notification));
            }

            return Task.CompletedTask;
        }
    }
}