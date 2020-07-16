using Codeizi.Curso.Domain.SharedKernel.Events;

namespace Codeizi.Curso.infra.Data.EventSource.EventSource
{
    public class RavenDBEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public RavenDBEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<T>(T theEvent)
            where T : Event
        {
            _eventStoreRepository.Store(theEvent);
        }
    }
}