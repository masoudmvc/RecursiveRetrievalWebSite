using Microsoft.Extensions.DependencyInjection;
using RecursiveRetrievalWebSite.Service.Services;
using RecursiveRetrievalWebSite.Service.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecursiveRetrievalWebSite.Infrastructure
{
    public class ServiceRegistration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<Program>();
            services.AddScoped<IRecursiveRetrievalService, RecursiveRetrievalService>();
            services.AddScoped<IInternetService, InternetService>();
        }
    }
}
