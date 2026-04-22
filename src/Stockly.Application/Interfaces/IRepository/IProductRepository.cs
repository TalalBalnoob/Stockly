using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.IRepository;

public interface IProductRepository {
	Task<IEnumerable<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(Guid id);
	Task<Product> GetByNameAsync(string name);
	Task<Product> AddAsync(Product product);
	Task<Product> UpdateAsync(Product product);
	Task<Product> AdjustStockAsync(Guid productId, int change);
	Task DeleteAsync(Guid id);
}

