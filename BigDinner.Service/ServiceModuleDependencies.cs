using BigDinner.Service.Authentication;
using BigDinner.Service.Date;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BigDinner.Service
{
    public static class ServiceModuleDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection(JWT.SectionName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUserClaimsService, UserClaimsService>();
            services.AddSingleton<IDateTimeProvider, DateTimeprovider>();
            return services;
        }

    }
}
