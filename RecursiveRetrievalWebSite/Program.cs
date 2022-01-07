using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecursiveRetrievalWebSite.Infrastructure;
using RecursiveRetrievalWebSite.Service.Services.Contracts;

namespace RecursiveRetrievalWebSite
{
    public class Program
    {
        private readonly ILogger<Program> _logger;
        private readonly IRecursiveRetrievalService _rrService;

        public Program(ILogger<Program> logger, IRecursiveRetrievalService rrService)
        {
            _logger = logger;
            _rrService = rrService;
        }

        static void Main(string[] args)
        {
            var host = HostConfiguration.CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Program>().Run(args);
        }

        public void Run(string[] args)
        {
            _logger.LogInformation("Logger service active...");

            var diskLocationToSaveFiles = @"C:\MasCodeChallange";

            _rrService.TraverseAndDownload(diskLocationToSaveFiles);
        }
    }
}
