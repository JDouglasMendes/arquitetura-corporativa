using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using Codeizi.Curso.RH.infra.Data.EventSource.EventSource;

namespace Codeizi.Curso.RH.infra.Data.EventSource.EventStore
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