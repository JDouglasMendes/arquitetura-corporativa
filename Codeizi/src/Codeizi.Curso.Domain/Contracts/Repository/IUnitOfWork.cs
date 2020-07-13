using System;

namespace Codeizi.Curso.Domain.Contracts.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
