using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure;

public class AppDbContext : DbContext {
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<StockAdjustment>()
			.HasOne(sa => sa.Order)
			.WithMany()
			.HasForeignKey(sa => sa.OrderId)
			.OnDelete(DeleteBehavior.SetNull);
	}


	public DbSet<Product> Products { get; set; }
	public DbSet<Stock> Stocks { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<StockAdjustment> StockAdjustments { get; set; }
}