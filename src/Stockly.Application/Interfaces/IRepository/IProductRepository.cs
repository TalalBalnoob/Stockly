using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.IRepository;

public interface IProductRepository {
	Task<IEnumerable<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(Guid id);
	Task<IEnumerable<Product>> GetByNameAsync(string name);
	Task<Product> AddAsync(Product product);
	Task<Product> UpdateAsync(Product product);
	void DeleteAsync(Guid id);
}