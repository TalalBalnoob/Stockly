using Stockly.Application.DTOs.Products;
using Stockly.Application.Exceptions;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Products;
using Stockly.Domain.Entities;

namespace Stockly.Application.UseCases.Products;


public class UpdateProductUseCase(IProductsRepo productsRepo) : IUpdateProductUseCase {


	public async Task<ProductResponseDto> ExecuteAsync(Guid id, UpdateProductDto dto) {
		var product = await productsRepo.GetByIdAsync(id);

		if (product == null) {
			throw new NotFoundException($"Product with id {id} not found.");
		}

		if (dto.Name != null) product.Name = dto.Name;
		product.Price = dto.Price;
		product.Quantity = dto.Quantity;
		if (dto.Description != null) product.Description = dto.Description;
		if (dto.StorageNote != null) product.StorageNote = dto.StorageNote;
		product.IsAvailable = dto.IsAvailable;

		Product updatedProduct = new Product {
			Id = product.Id,
			Name = product.Name,
			Price = product.Price,
			Quantity = product.Quantity,
			Description = product.Description,
			StorageNote = product.StorageNote,
			IsAvailable = product.IsAvailable
		};

		return await productsRepo.UpdateAsync(updatedProduct);
	}
}
