using Codeizi.Curso.RH.Domain.SharedKernel.Events;
using Codeizi.Curso.RH.infra.Data.EventSource.Context;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.infra.Data.EventSource.EventSource
{
    public sealed class EventStoreRavenDBRepository : IEventStoreRepository
    {
        private readonly DocumentStoreHolder _context;

        public EventStoreRavenDBRepository(DocumentStoreHolder context)
            => _context = context;

        public void Store(object theEvent)
        {
            _context.Session.Store(theEvent);
            _context.Session.SaveChanges();
        }

        public async Task<IList<T>> All<T>(Guid aggregateId) where T : Event
            => await _context.Session.Query<T>().Where(x => x.AggregateId == aggregateId).ToListAsync();

        public void Dispose()
            => _context.Dispose();
    }
}