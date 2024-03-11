using System.Reflection;

namespace BigDinner.API;

public static class ApiModuleDependencies
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services;
    }
}

