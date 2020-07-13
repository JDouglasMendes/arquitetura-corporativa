﻿using Codeizi.Curso.Domain.SharedKernel.Commands;
using Codeizi.Curso.Domain.SharedKernel.Events;
using Codeizi.Curso.Domain.SharedKernel.IMediatorBus;
using MediatR;
using System.Threading.Tasks;

namespace Codeizi.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command)
            where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event)
            where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
            {
                _eventStore?.Save(@event);
            }

            return _mediator.Publish(@event);
        }
    }
}
