using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Scheduler
{
    public class ServiceProviderResolver
    {
        public ServiceProviderResolver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get; }
    }
}
