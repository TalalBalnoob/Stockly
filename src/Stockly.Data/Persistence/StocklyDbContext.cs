using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Entities;

namespace Stockly.Data.Persistence;

public class StocklyDbContext : DbContext
{
    public StocklyDbContext(DbContextOptions<StocklyDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<StockAdjustment> StockAdjustments { get; set; }

}
