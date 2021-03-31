using RH.Domain.Contracts.Repository;
using RH.Infra.Data.Context;

namespace RH.Infra.Data.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly CodeiziContext _context;

        public UnitOfWork(CodeiziContext context)
            => _context = context;

        public bool Commit()
            => _context.SaveChanges() > 0;

        public void Dispose()
            => _context.Dispose();
    }
}