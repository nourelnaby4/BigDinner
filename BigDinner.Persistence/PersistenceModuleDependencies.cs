using BigDinner.Domain.Models.Menus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BigDinner.Persistence;

public static class PersistenceModuleDependencies
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepo, UserRepo>();

        services.AddScoped<IMenuRepository, MenuRepository>();

        return services;
    }
}

