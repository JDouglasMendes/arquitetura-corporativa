using Codeizi.Curso.Domain.Contracts.Repository;
using Codeizi.Curso.Infra.Data.Context;

namespace Codeizi.Curso.Infra.Data.UnitOfWork
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
