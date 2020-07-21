using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.RH.Domain.Colaboradores.Events;
using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Domain.Colaboradores.EventHandlers
{
    public class ColaboradorEventHandler :
       INotificationHandler<ColaboradorAdmitidoEvent>,
        INotificationHandler<ColaboradorAdmitidoEventSource>
    {
        private readonly IRabbitMQBus _rabbitMQBus;
        private readonly IEventStore _eventStore;

        public ColaboradorEventHandler(IRabbitMQBus rabbitMQBus, IEventStore eventStore)
        {
            _rabbitMQBus = rabbitMQBus;
            _eventStore = eventStore;
        }


        public Task Handle(ColaboradorAdmitidoEvent request, CancellationToken cancellationToken)
        {
            _rabbitMQBus.Publisher(FactoryPublishable.Get(request.AggregateId,
                                                         "Add-contrato",
                                                         request));
            return Task.CompletedTask;
        }

        public Task Handle(ColaboradorAdmitidoEventSource notification, CancellationToken cancellationToken)
        {
            if (notification.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(notification);

            return Task.CompletedTask;
        }
    }
}