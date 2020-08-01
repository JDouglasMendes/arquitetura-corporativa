using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codeizi.Curso.APIGateway
{
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable

    public class Program
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
#pragma warning disable IDE0060 // Remove unused parameter

        public static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration(ic => ic.AddJsonFile("ocelot.json"))
                    .UseStartup<Startup>();
    }
}