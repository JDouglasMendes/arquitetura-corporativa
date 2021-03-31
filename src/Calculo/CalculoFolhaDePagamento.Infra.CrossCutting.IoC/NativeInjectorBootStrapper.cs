using CalculoFolhaDePagamento.Domain.Services.Repositories;
using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using CalculoFolhaDePagamento.Infra.Data.Repositories;
using CalculoFolhaDePagamento.Infra.Data.Services;
using Infra.CrossCutting.Configuration;
using Infra.CrossCutting.Identity;
using Infra.CrossCutting.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace CalculoFolhaDePagamento.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICodeiziConfiguration, CodeiziConfiguration>();
            services.AddScoped<MultiplexerRedis>();
            services.AddScoped<DatabaseRedis>();
            services.AddScoped<ICalculoRepository, CalculoRepository>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            services.AddScoped<IFeedbackExecucaoCalculoServiceDomain, FeedbackExecucaoCalculoServiceDomain>();
            services.AddScoped<IUser, UserContext>();
        }
    }
}