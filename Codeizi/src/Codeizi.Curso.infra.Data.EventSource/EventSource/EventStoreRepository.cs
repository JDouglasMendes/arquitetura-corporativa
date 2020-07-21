using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using Codeizi.Curso.RH.infra.Data.EventSource.Context;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.infra.Data.EventSource.EventSource
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly DatabaseEventSource _databaseEventSource;

        public EventStoreRepository(DatabaseEventSource databaseEventSource)
            => _databaseEventSource = databaseEventSource;

        public async Task<IEnumerable<T>> All<T>(string aggregateId) where T : Event
        {
            var collection = _databaseEventSource.Get().GetCollection<T>(typeof(T).Name);
            return await collection.Find(x => x.AggregateId == aggregateId).ToListAsync();
        }

        public async Task Store<T>(T theEvent) where T : Event
        {
            var collection = _databaseEventSource.Get().GetCollection<T>(theEvent.MessageType);
            await collection.InsertOneAsync(theEvent);
        }
    }
}