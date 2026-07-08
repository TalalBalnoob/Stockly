using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stockly.Application.Interfaces.Repositories;
using Stockly.Data.Persistence;
using Stockly.Data.Repositories;

namespace Stockly.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<StocklyDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));


        services.AddScoped<IProductsRepo, ProductsRepo>();
        services.AddScoped<IOrdersRepo, OrdersRepo>();
        services.AddScoped<IStockAdjustmentsRepo, StockAdjustmentsRepo>();

        return services;
    }
}
