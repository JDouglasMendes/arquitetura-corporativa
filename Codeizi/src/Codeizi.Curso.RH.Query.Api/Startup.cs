using Codeizi.Curso.infra.CrossCutting.EventBusRabbitMQ;
using Codeizi.Curso.Infra.CrossCutting.Configuration;
using Codeizi.Curso.RH.Query.Api.Bus;
using Codeizi.Curso.RH.Query.Api.Context;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Codeizi.Curso.RH.Query.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
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
                                            typeof(ContratoQueryServiceBus),
                                            queueName: "contrato-query");
            });

            services.AddSingleton<ICodeiziConfiguration, CodeiziConfiguration>();
            services.AddScoped<DatabaseQuery>();
            services.AddScoped<ContratoQueryServiceBus>();
        }
        
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            _ = app.ApplicationServices.GetService<IRabbitMQBus>();
        }
    }
}