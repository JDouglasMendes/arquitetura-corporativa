using System;

namespace RH.Domain.Contracts.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}