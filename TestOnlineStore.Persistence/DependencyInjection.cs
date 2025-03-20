using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestOnlineStore.Persistence.Repositories;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DbConnection");
        
        services.AddDbContext<TestOnlineStoreDBContext>(options =>
            options.UseSqlServer(connection));

        services.AddScoped<IRepositoryCategory, RepositoryCategory>();
        services.AddScoped<IRepositoryProduct, RepositoryProduct>();

        return services;
    }
}
