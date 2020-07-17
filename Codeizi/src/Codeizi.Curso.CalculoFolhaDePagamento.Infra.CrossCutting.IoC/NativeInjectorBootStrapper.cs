using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICalculoRepository, CalculoRepository>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
        }
    }
}