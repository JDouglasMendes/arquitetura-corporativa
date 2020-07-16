using MediatR;
using System;

namespace Codeizi.Curso.Domain.SharedKernel.Events
{
    public abstract class Message : IRequest<bool>
    {
        public string Id { get; set; }
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}