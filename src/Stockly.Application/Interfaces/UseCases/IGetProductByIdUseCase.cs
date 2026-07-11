using Stockly.Application.DTOs.Products;

namespace Stockly.Application.Interfaces.UseCases;

public interface IGetProductByIdUseCase {
	Task<ProductResponseDto> ExecuteAsync(Guid productId);
}
