using Domain.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.infra.Data.EventSource.EventSource
{
    public interface IEventStoreRepository
    {
        Task Store<T>(T theEvent) where T : Event;

        Task<IEnumerable<T>> All<T>(Guid aggregateId) where T : Event;
    }
}