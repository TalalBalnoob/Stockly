using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure;

public class AppDbContext : DbContext {
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
	}

	public DbSet<Product> Products { get; set; }
	public DbSet<Stock> Stocks { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<StockAdjustment> StockAdjustments { get; set; }
}