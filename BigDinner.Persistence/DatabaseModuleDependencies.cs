using BigDinner.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BigDinner.Persistence
{
    public static class DatabaseModuleDependencies
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
        {
          
            return services;
        }
    }
}
