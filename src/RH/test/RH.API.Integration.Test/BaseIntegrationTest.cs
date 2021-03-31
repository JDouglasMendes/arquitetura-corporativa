using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using RH.Service.Api;
using System.Net.Http;

namespace Api.Integration.Test
{
    public class BaseIntegrationTest
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }

        protected BaseIntegrationTest()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            Client = Server.CreateClient();
        }
    }
}