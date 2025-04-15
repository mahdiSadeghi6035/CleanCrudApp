using Application.Contracts.Infrastructure.Repositories;
using Infrastructure.Persistance.Context;
using Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistance;

public static class DependencyInjection
{
    public static void RegisterPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ProductDbContext>(p => p.UseSqlServer(connectionString));


        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
