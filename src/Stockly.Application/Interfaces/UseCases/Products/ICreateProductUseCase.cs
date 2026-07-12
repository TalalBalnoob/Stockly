using Stockly.Application.DTOs.Products;

namespace Stockly.Application.Interfaces.UseCases.Products;

interface ICreateProductUseCase {
	Task<ProductResponseDto> ExecuteAsync(CreateProductDto dto);
}
