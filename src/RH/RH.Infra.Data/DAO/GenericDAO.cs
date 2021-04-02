using RH.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Infra.Data.DAO
{
    public abstract class GenericDao<TEntity> : IGenericDao<TEntity>
        where TEntity : class
    {
        private readonly CodeiziContext db;
        private readonly DbSet<TEntity> dbSetContext;
        private bool disposedValue;

        protected GenericDao(CodeiziContext db)
        {
            this.db = db;
            dbSetContext = this.db.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
            => await dbSetContext.AddAsync(entity);

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
            => await dbSetContext.FindAsync(id).AsTask();

        public virtual IQueryable<TEntity> GetQueryable()
            => dbSetContext;

        public virtual void Update(TEntity obj)
            => dbSetContext.Update(obj);

        public virtual async Task RemoveAsync(Guid id)
            => dbSetContext.Remove(await GetByIdAsync(id));

        public async Task<int> SaveChangesAsync()
            => await db.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    db.Dispose();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}