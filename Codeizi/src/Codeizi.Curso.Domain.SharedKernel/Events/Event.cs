using MediatR;
using System;

namespace Codeizi.Curso.Domain.SharedKernel.Events
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