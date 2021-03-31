using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RH.Service.Api.Extensions
{
    public static class AutheticationExtensions
    {
        public static IServiceCollection AddIdentityServerAuthetication(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                      .AddJwtBearer(options =>
                      {
                          options.Authority = "http://localhost:5000";
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

            return services;
        }
    }
}