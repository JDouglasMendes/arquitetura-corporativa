using Codeizi.Curso.Domain.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codeizi.Curso.infra.Data.EventSource.EventSource
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(object theEvent);

        Task<IList<T>> All<T>(Guid aggregateId) where T : Event;
    }
}