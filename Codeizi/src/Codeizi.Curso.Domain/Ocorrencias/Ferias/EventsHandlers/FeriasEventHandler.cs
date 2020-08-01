using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Domain.Ocorrencias.Ferias.EventsHandlers
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