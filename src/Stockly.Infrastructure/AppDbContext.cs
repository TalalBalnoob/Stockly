using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure;

public class AppDbContext : DbContext {
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
	}

	DbSet<Product> Products { get; set; }
	DbSet<Stock> Stocks { get; set; }
	DbSet<Order> Orders { get; set; }
	DbSet<OrderItem> OrderItems { get; set; }
	DbSet<StockAdjustment> StockAdjustments { get; set; }
}