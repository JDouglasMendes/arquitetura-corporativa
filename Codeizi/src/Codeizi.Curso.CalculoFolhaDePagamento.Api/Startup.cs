using Codeizi.Curso.CalculoFolhaDePagamento.Infra.CrossCutting.IoC;
using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Codeizi.Curso.CalculoFolhaDePagamento.Api
{
    public sealed class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

#pragma warning disable CA1822 // Mark members as static

        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Mark members as static
        {
            services.AddHttpContextAccessor();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.Authority = Configuration.GetValue<string>("Authority");
                  options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                  {
                      ValidateAudience = false,
                  };
              });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddControllers();

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                    factory.UserName = Configuration["EventBusUserName"];

                if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                    factory.Password = Configuration["EventBusPassword"];

                if (!string.IsNullOrEmpty(Configuration["EventBusPort"]))
                    factory.Port = int.Parse(Configuration["EventBusPort"]);

                var retryCount = 5;

                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IRabbitMQBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();

                return new EventBusRabbitMQ(rabbitMQPersistentConnection,
                                            logger,
                                            services,
                                            typeof(Program),
                                            queueName: "notificar-usuario");
            });

            NativeInjectorBootStrapper.RegisterServices(services);


        }

#pragma warning disable CA1822 // Mark members as static

        public void Configure(IApplicationBuilder app)
#pragma warning restore CA1822 // Mark members as static
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