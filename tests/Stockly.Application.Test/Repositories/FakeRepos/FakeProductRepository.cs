using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;


namespace Stockly.Infrastructure.Test;

public class FakeProductRepository : IProductRepository {
	private readonly List<Product> db;

	public FakeProductRepository() {
		this.db = [];
	}

	public async Task<IEnumerable<Product>> GetAllAsync() {
		return db.ToList();
	}

	public async Task<Product?> GetByIdAsync(Guid id) {
		return db.Find(p => id == p.Id);
	}

	public async Task<Product> GetByNameAsync(string name) {
		return db.First(p => p.Name == name);
	}

	public async Task<Product> AddAsync(Product product) {
		db.Add(product);

		return product;
	}

	public async Task<Product> UpdateAsync(Product product) {
		var index = db.FindIndex(p => p.Id == product.Id);
		if (index != -1) db[index] = product;
		return product;
	}

	public async void DeleteAsync(Guid id) {
		var product = await GetByIdAsync(id);
		if (product == null) return;
		db.Remove(product);
	}
}