using Infra.CrossCutting.EventBusRabbitMQ;
using MediatR;
using RH.Domain.Ocorrencias.Ferias.Events;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Domain.Ocorrencias.Ferias.EventsHandlers
{
    public class FeriasEventHandler :
        INotificationHandler<AgendamentoDeFeriasQueryEvent>
    {
        private readonly IRabbitMQBus _rabbitMQBus;

        public FeriasEventHandler(IRabbitMQBus rabbitMQBus)
            => _rabbitMQBus = rabbitMQBus;

        public async Task Handle(AgendamentoDeFeriasQueryEvent notification, CancellationToken cancellationToken)
            => await _rabbitMQBus.Publisher(FactoryPublishable.Get(notification.AggregateId,
                                                                   "agendamento-ferias-query",
                                                                   notification));
    }
}