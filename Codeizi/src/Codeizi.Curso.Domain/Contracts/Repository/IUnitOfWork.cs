using System;

namespace Codeizi.Curso.RH.Domain.Contracts.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}