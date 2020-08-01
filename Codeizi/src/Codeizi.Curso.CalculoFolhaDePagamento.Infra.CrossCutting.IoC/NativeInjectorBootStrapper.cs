using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Services;
using Codeizi.Curso.Infra.CrossCutting.Configuration;
using Codeizi.Curso.Infra.CrossCutting.Identity;
using Codeizi.Curso.Infra.CrossCutting.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICodeiziConfiguration, CodeiziConfiguration>();
            services.AddSingleton<MultiplexerRedis>();
            services.AddSingleton<DatabaseRedis>();            
            services.AddScoped<ICalculoRepository, CalculoRepository>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            
            services.AddSingleton<IFeedbackExecucaoCalculoServiceDomain, FeedbackExecucaoCalculoServiceDomain>();
            services.AddScoped<IUser, UserContext>();
        }
    }
}