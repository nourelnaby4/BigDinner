using BigDinner.Persistence.Context;
using BigDinner.Persistence.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BigDinner.Persistence;

public static class DatabaseModuleDependencies
{
    public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectioString = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton<ConvertDomainEventToOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, optionBuilder) =>
        {
            var interceptor = serviceProvider.GetService<ConvertDomainEventToOutboxMessagesInterceptor>();

            optionBuilder.UseSqlServer(connectioString)
            .AddInterceptors(interceptor);

        });

        services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services.AddIdentity<ApplicationUser, IdentityRole>(option =>
        {
            // Password settings.
            option.Password.RequireDigit = false;
            option.Password.RequireLowercase = false;
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequireUppercase = false;
            option.Password.RequiredLength = 6;
            option.Password.RequiredUniqueChars = 0;

            // Lockout settings.
            option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            option.Lockout.MaxFailedAccessAttempts = 5;
            option.Lockout.AllowedForNewUsers = true;

            // User settings.
            option.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            option.User.RequireUniqueEmail = true;
            option.SignIn.RequireConfirmedEmail = false;

        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}
