using BigDinner.Application.Common.Abstractions.Emails;
using BigDinner.Service.Authentication;
using BigDinner.Service.Date;
using BigDinner.Service.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace BigDinner.Service;

public static class ServiceModuleDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWT>(configuration.GetSection(JWT.SectionName));

        services.Configure<EmailSetting>(configuration.GetSection(EmailSetting.SectionName));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailSetting>>().Value);

        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped<IUserClaimsService, UserClaimsService>();

        services.AddSingleton<IDateTimeProvider, DateTimeprovider>();
        return services;
    }

}
