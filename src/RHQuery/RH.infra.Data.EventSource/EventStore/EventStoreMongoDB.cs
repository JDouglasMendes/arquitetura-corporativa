using Domain.SharedKernel.Events;
using RH.infra.Data.EventSource.EventSource;

namespace RH.infra.Data.EventSource.EventStore
{
    public class EventStoreMongoDB : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStoreMongoDB(IEventStoreRepository eventStoreRepository)
            => _eventStoreRepository = eventStoreRepository;

        public void Save<T>(T theEvent) where T : Event
         => _eventStoreRepository.Store(theEvent);
    }
}