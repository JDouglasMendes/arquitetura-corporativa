using MediatR;
using System;

namespace Domain.SharedKernel.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}