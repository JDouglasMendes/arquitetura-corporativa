using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.Repositories;
using Codeizi.Curso.CalculoFolhaDePagamento.Domain.Services.ServiceDomain;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.BackgroundTasks.Extensions;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.BackgroundTasks.Tasks;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Infra.BackgroundTasks
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
            services.AddScoped<NovoContratoServicoBus>();
            services.AddScoped<ICalculoRepository, CalculoRepository>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            services.AddEventBus(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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