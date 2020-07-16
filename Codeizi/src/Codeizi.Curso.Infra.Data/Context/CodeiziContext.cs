using Codeizi.Curso.Infra.Data.Map;
using Microsoft.EntityFrameworkCore;

namespace Codeizi.Curso.Infra.Data.Context
{
    public class CodeiziContext : DbContext
    {
        public CodeiziContext(DbContextOptions<CodeiziContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ColaboradorMap());
            modelBuilder.ApplyConfiguration(new ContratoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}