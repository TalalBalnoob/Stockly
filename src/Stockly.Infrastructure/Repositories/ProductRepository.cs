using Microsoft.EntityFrameworkCore;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class ProductRepository(AppDbContext db) : IProductRepository {
	public async Task<IEnumerable<Product>> GetAllAsync() {
		return db.Products.ToList();
	}

	public async Task<IEnumerable<Product>> GetAllWithStockAsync() {
		return db.Products.Include(p => p.Stock).ToList();
	}

	public async Task<Product?> GetByIdAsync(Guid id) {
		return await db.Products.FindAsync(id);
	}

	public async Task<Product?> GetByIdWithStockAsync(Guid id) {
		return db.Products.Include(p => p.Stock).FirstOrDefault(p => p.Id == id);
	}

	public async Task<Product> GetByNameAsync(string name) {
		return db.Products.First(p => p.Name == name);
	}

	public async Task<Product> GetByNameWithStockAsync(string name) {
		return db.Products.Include(p => p.Stock).First(p => p.Name == name);
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

	public async Task DeleteAsync(Guid id) {
		var product = await GetByIdAsync(id);
		db.Products.Remove(product);
		await db.SaveChangesAsync();
	}
}