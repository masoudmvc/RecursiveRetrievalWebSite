using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecursiveRetrievalWebSite.Infrastructure;
using RecursiveRetrievalWebSite.Service.Services.Contracts;
using System;

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
            //var test = _internet.GetHtml(@"https://tretton37.com/");
            _rrService.TraverseAndDownload(@"https://tretton37.com/", @"C:\Drive-D\WorkArea\pak");
        }
    }
}
