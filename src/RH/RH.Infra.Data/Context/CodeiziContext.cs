using RH.Infra.Data.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace RH.Infra.Data.Context
{
    public class CodeiziContext : DbContext
    {
        public CodeiziContext(DbContextOptions<CodeiziContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ColaboradorMap());
            modelBuilder.ApplyConfiguration(new ContratoMap());
            modelBuilder.ApplyConfiguration(new OcorrenciaDeFeriasMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}