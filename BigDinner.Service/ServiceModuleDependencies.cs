using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Application.Common.Abstractions.Emails;
using BigDinner.Service.Authentication;
using BigDinner.Service.Cache;
using BigDinner.Service.Date;
using BigDinner.Service.Emails;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using BigDinner.Application.Common.Abstractions.JsonSerialize;


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

        #region cache
        var cacheExpirationMinutes = configuration.GetSection("CacheSettings")
            .GetValue<int>("DefaultCacheExpirationMinutes");

        services.AddScoped<IMemoryCacheService, MemoryCacheService>();
        services.AddMemoryCache(options =>
        {
            options.ExpirationScanFrequency = TimeSpan.FromMinutes(5); // Optional: Set the frequency for scanning expired items
        });

        services.Configure<MemoryCacheEntryOptions>(options =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheExpirationMinutes); // Set default expiration time to 30 minutes
        });
        #endregion

        services.AddSingleton(new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = false,

        });

        services.AddSingleton(new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        }) ;

        services.AddScoped<IRedisCacheService, RedisCacheService>();

        return services;
    }

}
