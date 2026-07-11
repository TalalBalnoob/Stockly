using Stockly.Application.DTOs.Products;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Domain.Entities;

namespace Stockly.Application.UseCases;

class GetProductsUseCase(IProductsRepo productRepo) : IGetProductsUseCase {
	public async Task<IEnumerable<ProductResponseDto>> ExecuteAsync(ProductQueryParams queryParams) {
		IEnumerable<ProductResponseDto> products = await productRepo.GetAllAsync();

		// Apply filters based on query parameters
		// Filter by availability
		if (queryParams.IsAvailable.HasValue) {
			products = products.Where(p => p.IsAvailable == queryParams.IsAvailable.Value);
		}

		// Filter by search term
		if (!string.IsNullOrEmpty(queryParams.Search)) {
			products = products.Where(p => p.Name.Contains(queryParams.Search.ToLower(), StringComparison.OrdinalIgnoreCase));
		}

		// Filter by quantity range
		if (queryParams.MinQuantity.HasValue) {
			products = products.Where(p => p.Quantity >= queryParams.MinQuantity.Value);
		}

		if (queryParams.MaxQuantity.HasValue) {
			products = products.Where(p => p.Quantity <= queryParams.MaxQuantity.Value);
		}

		return products;
	}
}
