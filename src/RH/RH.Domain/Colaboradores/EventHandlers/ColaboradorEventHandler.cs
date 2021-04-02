﻿using Domain.SharedKernel.Events;
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
        private readonly IEventStore _eventStore;

        public ColaboradorEventHandler(IRabbitMQBus rabbitMQBus, IEventStore eventStore)
        {
            _rabbitMQBus = rabbitMQBus;
            _eventStore = eventStore;
        }

        public Task Handle(NovoColaboradorParaCalculoEvent notification, CancellationToken cancellationToken)
        {
            _rabbitMQBus.Publisher(FactoryPublishable.Get(notification.AggregateId,
                                                         "add-contrato",
                                                         notification));
            return Task.CompletedTask;
        }

        public Task Handle(ColaboradorEventSource notification, CancellationToken cancellationToken)
        {
            if (!notification.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(notification);

            return Task.CompletedTask;
        }

        public Task Handle(ContratoQueryEvent notification, CancellationToken cancellationToken)
        {
            _rabbitMQBus.Publisher(FactoryPublishable.Get(notification.AggregateId,
                                                         "contrato-query",
                                                         notification));
            return Task.CompletedTask;
        }
    }
}