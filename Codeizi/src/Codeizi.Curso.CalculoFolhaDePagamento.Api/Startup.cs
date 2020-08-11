using Codeizi.Curso.CalculoFolhaDePagamento.Api.Extensions;
using Codeizi.Curso.CalculoFolhaDePagamento.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Api
{
    public sealed class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddAuthentication(Configuration);
            services.AddControllers();
            services.AddEventBus(Configuration);
            NativeInjectorBootStrapper.RegisterServices(services);
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
        }
    }
}