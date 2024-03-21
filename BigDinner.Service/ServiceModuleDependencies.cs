using BigDinner.Application.Common.Interfaces.Authentication;
using BigDinner.Application.Common.Interfaces.Date;
using BigDinner.Application.Common.Models;
using BigDinner.Service.Authentication;
using BigDinner.Service.Date;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Service
{
    public static class ServiceModuleDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection(JWT.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeprovider>();
            return services;
        }

    }
}
