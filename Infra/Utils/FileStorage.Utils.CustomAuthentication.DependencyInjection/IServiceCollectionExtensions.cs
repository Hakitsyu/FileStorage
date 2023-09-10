using FileStorage.UserAuthentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace FileStorage.HeaderAuthentication.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, Action<CustomAuthenticationSchemeOptions> configureOptions) 
        {
            services
                .AddAuthentication()
                .AddScheme<CustomAuthenticationSchemeOptions, CustomAuthenticationSchemeHandler>(CustomAuthenticationScheme.Name,
                    configureOptions);

            return services;
        }
    }
}
