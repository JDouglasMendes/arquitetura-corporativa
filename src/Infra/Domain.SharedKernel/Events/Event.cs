using MediatR;
using System;
using System.Collections.Generic;

namespace Domain.SharedKernel.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        public abstract string GetKeyQueues { get; }

        protected Event()
            => Timestamp = DateTime.Now;
    }
}