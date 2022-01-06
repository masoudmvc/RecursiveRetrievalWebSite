using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecursiveRetrievalWebSite.Infrastructure;
using System;

namespace RecursiveRetrievalWebSite
{
    public class Program
    {
        private readonly ILogger<Program> _logger;
        private readonly ICustomer _customer;

        public Program(ILogger<Program> logger, ICustomer customer)
        {
            _logger = logger;
            _customer = customer;
        }

        static void Main(string[] args)
        {
            var host = HostConfiguration.CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Program>().Run(args);
        }

        public void Run(string[] args)
        {
            var test = _customer.GetName();
        }
    }
}
