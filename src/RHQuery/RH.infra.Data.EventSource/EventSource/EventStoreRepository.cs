using Domain.SharedKernel.Events;
using MongoDB.Driver;
using RH.infra.Data.EventSource.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.infra.Data.EventSource.EventSource
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly DatabaseEventSource _databaseEventSource;

        public EventStoreRepository(DatabaseEventSource databaseEventSource)
            => _databaseEventSource = databaseEventSource;

        public async Task<IEnumerable<T>> All<T>(Guid aggregateId) where T : Event
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