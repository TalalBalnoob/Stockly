using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.DTOs;
using Stockly.Domain.Entity;

namespace Stockly.Application.UseCases;

public class UpdateProductUseCase {
	private readonly IProductRepository _productRepository;

	public UpdateProductUseCase(IProductRepository productRepository) {
		this._productRepository = productRepository;
	}

	public async Task<Product> Execute(UpdateProductDTO updateProduct) {
		Product? existingProduct = await this._productRepository.GetByIdAsync(Guid.Parse(updateProduct.Id));
		if (existingProduct == null) throw new Exception("Product not found");

		Product updatedProduct = await this._productRepository.UpdateAsync(new Product {
			Id = Guid.Parse(updateProduct.Id),
			Name = updateProduct.Name,
			Description = updateProduct.Description ?? string.Empty,
			Price = updateProduct.Price,
			Quantity = updateProduct.Quantity,
			IsActive = true
		});
		return updatedProduct;
	}
}
