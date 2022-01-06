using Microsoft.Extensions.DependencyInjection;
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
            services.AddTransient<ICustomer, Customer>();
        }
    }

    #region test classes
    public interface ICustomer
    {
        string GetName();
    }
    public class Customer : ICustomer
    {
        public string GetName()
        {
            return "masoud";
        }
    }

    #endregion
}
