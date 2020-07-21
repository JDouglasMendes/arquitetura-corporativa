using MediatR;
using System;

namespace Codeizi.Curso.RH.Domain.SharedKernel.Events
{
    public abstract class Message : IRequest<bool>
    {
        public string Id { get; set; }
        public string MessageType { get; protected set; }
        public string AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}