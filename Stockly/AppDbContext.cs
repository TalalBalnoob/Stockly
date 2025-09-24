using Microsoft.EntityFrameworkCore;

using Stockly.Models;

namespace Stockly;

public class AppDbContext : DbContext {
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Product> Products { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<StockAdjustment> StockAdjustment { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		var seedDate = new DateTime(2025, 1, 1);
		// Seed initial products
		modelBuilder.Entity<Product>().HasData(
			new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Storage_Note = "Keep dry", Price = 1200, Quantity = 10, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Storage_Note = "Handle with care", Price = 800, Quantity = 20, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 3, Name = "Headphones", Description = "Noise cancelling headphones", Price = 200, Quantity = 30, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 4, Name = "Keyboard", Description = "Mechanical keyboard", Price = 100, Quantity = 40, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 5, Name = "Mouse", Description = "Wireless mouse", Price = 50, Quantity = 50, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 6, Name = "Monitor", Description = "24-inch full HD monitor", Price = 250, Quantity = 15, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 7, Name = "Printer", Description = "All-in-one printer", Price = 300, Quantity = 5, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 8, Name = "Tablet", Description = "10-inch Android tablet", Price = 400, Quantity = 12, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 9, Name = "Desk", Description = "Wooden office desk", Price = 150, Quantity = 7, IsActive = true, CreatedAt = seedDate },
			new Product { Id = 10, Name = "Chair", Description = "Ergonomic office chair", Price = 180, Quantity = 9, IsActive = true, CreatedAt = seedDate }
		);
	}

}
