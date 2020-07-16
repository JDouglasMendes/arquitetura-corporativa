using Codeizi.Curso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.Curso.Infra.Data.DAO
{
    public abstract class GenericDAO<TEntity> : IGenericDAO<TEntity>
        where TEntity : class
    {
        private readonly CodeiziContext db;
        private readonly DbSet<TEntity> dbSetContext;
        private bool disposedValue;

        protected GenericDAO(CodeiziContext db)
        {
            this.db = db;
            dbSetContext = this.db.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSetContext.AddAsync(entity);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await dbSetContext.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSetContext;
        }

        public virtual void Update(TEntity obj)
        {
            dbSetContext.Update(obj);
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            dbSetContext.Remove(await GetByIdAsync(id));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

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