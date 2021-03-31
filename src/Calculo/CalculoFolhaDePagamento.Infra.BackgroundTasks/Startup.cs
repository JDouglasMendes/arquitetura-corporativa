using CalculoFolhaDePagamento.Domain.Services.Repositories;
using CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using CalculoFolhaDePagamento.Infra.BackgroundTasks.Extensions;
using CalculoFolhaDePagamento.Infra.BackgroundTasks.Tasks;
using CalculoFolhaDePagamento.Infra.Data.Repositories;
using Infra.CrossCutting.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CalculoFolhaDePagamento.Infra.BackgroundTasks
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHostedService<ContratoParaCalculoBackgroundService>();
            services.AddScoped<ICodeiziConfiguration, CodeiziConfiguration>();
            services.AddScoped<NovoContratoServicoBus>();
            services.AddScoped<ICalculoRepository, CalculoRepository>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            services.AddEventBus(Configuration);
        }

#pragma warning disable CA1822 // Mark members as static

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore CA1822 // Mark members as static
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}