using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.IRepository;

public interface IProductRepository {
	Task<IEnumerable<Product>> GetAllAsync();
	Task<IEnumerable<Product>> GetAllWithStockAsync();
	Task<Product?> GetByIdAsync(Guid id);
	Task<Product?> GetByIdWithStockAsync(Guid id);
	Task<Product> GetByNameAsync(string name);
	Task<Product> AddAsync(Product product);
	Task<Product> UpdateAsync(Product product);
	Task DeleteAsync(Guid id);
}