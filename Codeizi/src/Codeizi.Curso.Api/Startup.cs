using AutoMapper;
using Codeizi.Curso.RH.Api.Extensions;
using Codeizi.Curso.RH.Application.AutoMapper;
using Codeizi.Curso.RH.Infra.CrossCutting.IoC;
using MediatR;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Codeizi.Curso.RH.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext(Configuration);
            services.AddControllers();
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
            services.AddEventBus(Configuration);
            services.AddIdentityServerAuthetication();
            services.AddSwaggerGen();
            services.AddMediatR(typeof(Startup));
            NativeInjectorBootStrapper.RegisterServices(services);
            if (Configuration.GetValue<bool>("isTest"))
                services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Codeizi Treinamento");
            });


        }
    }
}