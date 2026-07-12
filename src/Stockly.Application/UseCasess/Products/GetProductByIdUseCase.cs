using Stockly.Application.DTOs.Products;
using Stockly.Application.Exceptions;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Products;
using Stockly.Domain.Entities;

namespace Stockly.Application.UseCases.Products;

class GetProductByIdUseCase(IProductsRepo productRepo) : IGetProductByIdUseCase {
	public async Task<ProductResponseDto> ExecuteAsync(Guid productId) {
		ProductResponseDto product = await productRepo.GetByIdAsync(productId);

		if (product == null) {
			throw new NotFoundException($"Product with ID {productId} not found.");
		}

		return product;
	}
}
