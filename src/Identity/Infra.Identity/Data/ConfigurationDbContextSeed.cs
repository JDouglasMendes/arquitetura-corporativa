using Infra.Identity.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Identity.Data
{
    public static class ConfigurationDbContextSeed
    {
        public static async Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
        {
            var clientUrls = new Dictionary<string, string>
            {
                { "SPA", configuration.GetValue<string>("SpaClient") }
            };

            if (!context.Clients.Any())
            {
                await SaveClients(context, clientUrls);
            }
            else
            {
                await UpdateClients(context, clientUrls);
                await UpdateUrlClients(context);
            }

            await SaveResources(context);

            await SaveApiResources(context);

            await SaveApiScopes(context);
        }

        private static async Task SaveApiScopes(ConfigurationDbContext context)
        {
            if (!context.ApiScopes.Any())
            {
                foreach (var api in Config.GetApiScope())
                {
                    context.ApiScopes.Add(api.ToEntity());
                }
                await context.SaveChangesAsync();
            }
        }

        private static async Task SaveApiResources(ConfigurationDbContext context)
        {
            if (!context.ApiResources.Any())
            {
                foreach (var api in Config.GetApis())
                {
                    context.ApiResources.Add(api.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task SaveResources(ConfigurationDbContext context)
        {
            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.GetResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                await context.SaveChangesAsync();
            }
        }

        private static async Task UpdateUrlClients(ConfigurationDbContext context)
        {
            List<ClientRedirectUri> oldRedirects = (await context.Clients.Include(c => c.RedirectUris).ToListAsync())
                            .SelectMany(c => c.RedirectUris)
                            .Where(ru => ru.RedirectUri.EndsWith("/o2c.html"))
                            .ToList();

            if (oldRedirects.Any())
            {
                foreach (var ru in oldRedirects)
                {
                    ru.RedirectUri = ru.RedirectUri.Replace("/o2c.html", "/oauth2-redirect.html");
                    context.Update(ru.Client);
                }
                await context.SaveChangesAsync();
            }
        }

        private static async Task UpdateClients(ConfigurationDbContext context, Dictionary<string, string> clientUrls)
        {
            var clientExistentIds = context.Clients.Select(x => x.ClientId).ToList();
            var save = false;
            foreach (var client in Config.GetClients(clientUrls))
            {
                if (!clientExistentIds.Contains(client.ClientId))
                {
                    save = true;
                    context.Clients.Add(client.ToEntity());
                }
            }
            if (save)
                await context.SaveChangesAsync();
        }

        private static async Task SaveClients(ConfigurationDbContext context, Dictionary<string, string> clientUrls)
        {
            foreach (var client in Config.GetClients(clientUrls))
            {
                context.Clients.Add(client.ToEntity());
            }
            await context.SaveChangesAsync();
        }
    }
}