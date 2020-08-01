﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Infra.Data.DAO
{
    public interface IGenericDAO<TEntity> : IDisposable
        where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);

        IQueryable<TEntity> GetQueryable();

        void Update(TEntity obj);

        Task RemoveAsync(Guid id);

        Task<int> SaveChangesAsync();
    }
}