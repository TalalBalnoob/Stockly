using Stockly.Application.DTOs.Products;

namespace Stockly.Application.Interfaces.UseCases.Products;

public interface IGetProductByIdUseCase {
	Task<ProductResponseDto> ExecuteAsync(Guid productId);
}
