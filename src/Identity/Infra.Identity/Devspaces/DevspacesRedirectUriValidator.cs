﻿using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace Infra.Identity.Devspaces
{
    public class DevspacesRedirectUriValidator : IRedirectUriValidator
    {
        private readonly ILogger _logger;
        public DevspacesRedirectUriValidator(ILogger<DevspacesRedirectUriValidator> logger)
        {
            _logger = logger;
        }

        public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            return LogInformationRequest(requestedUri, client);
        }

        public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            return LogInformationRequest(requestedUri, client);
        }
        private Task<bool> LogInformationRequest(string requestedUri, Client client)
        {
            _logger.LogInformation("Client {ClientName} used post logout uri {RequestedUri}.", client.ClientName, requestedUri);
            return Task.FromResult(true);
        }

    }
}
