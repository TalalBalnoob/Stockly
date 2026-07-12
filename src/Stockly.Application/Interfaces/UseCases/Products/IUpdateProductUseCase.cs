using Stockly.Application.DTOs.Products;

namespace Stockly.Application.Interfaces.UseCases.Products;

interface IUpdateProductUseCase {
	Task<ProductResponseDto> ExecuteAsync(UpdateProductDto dto);
}
