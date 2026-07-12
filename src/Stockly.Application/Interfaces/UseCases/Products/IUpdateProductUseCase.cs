using Stockly.Application.DTOs.Products;

namespace Stockly.Application.Interfaces.UseCases.Products;

public interface IUpdateProductUseCase {
	Task<ProductResponseDto> ExecuteAsync(Guid id, UpdateProductDto dto);
}
