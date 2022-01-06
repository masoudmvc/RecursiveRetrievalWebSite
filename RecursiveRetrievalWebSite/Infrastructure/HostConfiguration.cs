using Microsoft.Extensions.Hosting;

namespace RecursiveRetrievalWebSite.Infrastructure
{
    public class HostConfiguration
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    ServiceRegistration.Configure(services);
                });
        }
    }
}
