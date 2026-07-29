using Stockly.Application.DTOs.Products;
using Stockly.Domain.Entities;

namespace Stockly.Application.Interfaces.Repositories;

public interface IProductsRepo {
	// Queries
	Task<IEnumerable<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(Guid id);
	Task<IEnumerable<Product>> GetByNameAsync(string name);

	// Filtered queries
	Task<IEnumerable<Product>> GetAvailableAsync();
	Task<IEnumerable<Product>> GetOutOfStockAsync();
	Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
	Task<IEnumerable<Product>> GetLowStockAsync(int threshold);

	// Commands
	Task<Product> AddAsync(Product product);
	Task<Product> UpdateAsync(Product product);
	Task DeleteAsync(Guid id);

	// Checks
	Task<bool> ExistsAsync(Guid id);

	abstract ProductResponseDto MapToResponseDto(Product product);
}
