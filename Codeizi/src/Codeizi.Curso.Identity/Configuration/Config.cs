using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Codeizi.Curso.Identity.Configuration
{
    public sealed class Config
    {


        public static IEnumerable<ApiScope> GetApiScope()
        {
            return new List<ApiScope>
            {
                new ApiScope("calculofolhadepagamento", "Codeizi Curso CalculoFolhaDePagamento Api"),
                new ApiScope("backgroundtasks", "Codeizi Curso CalculoFolhaDePagamento.Infra BackgroundTasks"),
                new ApiScope("signalrhub", "Codeizi Curso SignalrHub"),
                new ApiScope("rh", "Codeizi Curso RH Api"),
            };
        }

        // ApiResources define the apis in your system
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("calculofolhadepagamento", "Codeizi Curso CalculoFolhaDePagamento Api"),
                new ApiResource("backgroundtasks", "Codeizi Curso CalculoFolhaDePagamento.Infra BackgroundTasks"),
                new ApiResource("signalrhub", "Codeizi Curso SignalrHub"),
                new ApiResource("rh", "Codeizi Curso RH Api"),              
            };
        }

        // Identity resources are data like user ID, name, or email address of a user
        // see: http://docs.identityserver.io/en/release/configuration/resources.html
        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl)
        {
            return new List<Client>
            {
                // JavaScript Client
                new Client
                {
                    ClientId = "CodeiziClientJS",
                    ClientName = "Codeizi Client JS",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =           { $"{clientsUrl["SPA"]}/" },
                    RequireConsent = false,
                    PostLogoutRedirectUris = { $"{clientsUrl["SPA"]}/" },
                    AllowedCorsOrigins =     { $"{clientsUrl["SPA"]}" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "calculofolhadepagamento",
                        "backgroundtasks",
                        "signalrhub",
                        "rh",
                    },
                },
                new Client
                {
                    ClientName = "Postman",
                    AllowOfflineAccess = true,       
                    RedirectUris = new[]
                    {
                        "https://wwww.getpostman.com/oauth/callback"
                    },
                    Enabled = true,
                    ClientId = "f5129275-a10c-41c6-84c4-8d66210a4a8a",
                    ClientSecrets = new[]
                    {
                        new Secret("CodeiziSecrect".Sha256())
                    },
                    PostLogoutRedirectUris = new []
                    {
                        "https://localhost:5001"
                    },
                    ClientUri = null,
                    AllowedGrantTypes = new[]
                    {
                        GrantType.ResourceOwnerPassword
                    },
                    AllowedScopes =
                    {
                       IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "calculofolhadepagamento",
                        "backgroundtasks",
                        "signalrhub",
                        "rh",
                    },
                }
            };
        }
    }
}
