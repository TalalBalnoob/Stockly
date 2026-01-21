using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class ProductRepository(AppDbContext db) : IProductRepository {
	public async Task<IEnumerable<Product>> GetAllAsync() {
		return db.Products.ToList();
	}

	public async Task<Product?> GetByIdAsync(Guid id) {
		return await db.Products.FindAsync(id);
	}

	public async Task<IEnumerable<Product>> GetByNameAsync(string name) {
		return db.Products.Where(p => p.Name == name).ToList();
	}

	public async Task<Product> AddAsync(Product product) {
		db.Products.Add(product);
		await db.SaveChangesAsync();
		return product;
	}

	public async Task<Product> UpdateAsync(Product product) {
		db.Products.Update(product);
		await db.SaveChangesAsync();
		return product;
	}

	public async void DeleteAsync(Guid id) {
		var product = await GetByIdAsync(id);
		db.Products.Remove(product);
		await db.SaveChangesAsync();
	}
}