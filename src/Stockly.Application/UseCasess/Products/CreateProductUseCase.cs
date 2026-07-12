using Stockly.Application.DTOs.Products;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Products;
using Stockly.Domain.Entities;

namespace Stockly.Application.UseCases.Products;

class CreateProductUseCase(IProductsRepo productsRepo) : ICreateProductUseCase {

	public async Task<ProductResponseDto> ExecuteAsync(CreateProductDto dto) {
		var product = new Product {
			Id = Guid.NewGuid(),
			Name = dto.Name,
			Price = dto.Price,
			Quantity = dto.Quantity,
			Description = dto.Description,
			StorageNote = dto.StorageNote,
			IsAvailable = dto.IsAvailable,
			CreatedAt = DateTime.UtcNow
		};

		return await productsRepo.AddAsync(product);
	}
}
