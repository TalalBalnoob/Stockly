using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Domain.DTOs;
using Stockly.Domain.Entity;

namespace Stockly.Application.UseCases;

public class CreateProductUseCase {
	private readonly IProductRepository _productRepository;

	public CreateProductUseCase(IProductRepository productRepository) {
		this._productRepository = productRepository;
	}

	public async Task<Product> Execute(NewProductDTO newProduct) {
		Product product = new() {
			Name = newProduct.Name,
			Description = newProduct.Description ?? string.Empty,
			Price = newProduct.Price,
			Quantity = newProduct.Quantity,
			IsActive = true
		};
		Product createdProduct = await this._productRepository.AddAsync(product);
		return createdProduct;
	}
}
