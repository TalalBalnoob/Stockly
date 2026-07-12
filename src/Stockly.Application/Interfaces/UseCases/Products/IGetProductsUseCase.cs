using Stockly.Application.DTOs.Products;

namespace Stockly.Application.Interfaces.UseCases.Products;

public interface IGetProductsUseCase {
	Task<IEnumerable<ProductResponseDto>> ExecuteAsync(ProductQueryParams queryParams);
}
