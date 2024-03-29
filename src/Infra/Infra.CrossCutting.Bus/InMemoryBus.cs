﻿using Domain.SharedKernel.Commands;
using Domain.SharedKernel.Events;
using Domain.SharedKernel.IMediatorBus;
using MediatR;
using System.Threading.Tasks;

namespace Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
            => _mediator = mediator;

        public Task SendCommand<T>(T command)
            where T : Command
            => _mediator.Send(command);

        public Task RaiseEvent<T>(T @event)
            where T : Event
            => _mediator.Publish(@event);
    }
}