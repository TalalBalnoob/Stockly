using Stockly.Application.DTOs.Products;
using Stockly.Domain.Entities;

namespace Stockly.Application.Interfaces.Repositories;

public interface IProductsRepo {
	// Queries
	Task<IEnumerable<ProductResponseDto>> GetAllAsync();
	Task<ProductResponseDto?> GetByIdAsync(Guid id);
	Task<IEnumerable<ProductResponseDto>> GetByNameAsync(string name);

	// Filtered queries
	Task<IEnumerable<ProductResponseDto>> GetAvailableAsync();
	Task<IEnumerable<ProductResponseDto>> GetOutOfStockAsync();
	Task<IEnumerable<ProductResponseDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
	Task<IEnumerable<ProductResponseDto>> GetLowStockAsync(int threshold);

	// Commands
	Task<ProductResponseDto> AddAsync(Product product);
	Task<ProductResponseDto> UpdateAsync(Product product);
	Task DeleteAsync(Guid id);

	// Checks
	Task<bool> ExistsAsync(Guid id);
}
