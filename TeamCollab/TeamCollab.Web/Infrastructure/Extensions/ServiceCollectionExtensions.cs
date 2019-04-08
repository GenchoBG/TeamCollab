using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace TeamCollab.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            var serviceTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains(nameof(TeamCollab)))
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && t.Name.EndsWith("Service"))
                .ToList();

            foreach (var serviceType in serviceTypes)
            {
                var @interface = serviceType.GetInterfaces().FirstOrDefault(s => s.Name.Contains(serviceType.Name));

                services.AddTransient(@interface, serviceType);
            }

            return services;
        }
    }
}
