using Microsoft.Extensions.DependencyInjection;
using System;

namespace Myapi.Middlewares
{
    public static class ApiAuthorizedServicesExtensions
    {
        //public static IServiceCollection AddApiAuthorized(this IServiceCollection services)
        //{
        //    if (services == null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }

        //    return services;
        //}
        public static IServiceCollection AddApiAuthorized(this IServiceCollection services, Action<ApiAuthorizedOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            services.Configure(configureOptions);
            return services;
        }
    }
}
