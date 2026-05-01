using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.DTOs;
using Stockly.Domain.Entity;

namespace Stockly.Application.UseCases;

public class CreateProductUseCase {
	private readonly IProductRepository _productRepository;

	public CreateProductUseCase(IProductRepository productRepository) {
		_productRepository = productRepository;
	}
	public async Task Execute(NewProductDTO newProduct) {
		var product = new Product {
			Name = newProduct.Name,
			Description = newProduct.Description ?? string.Empty,
			Price = newProduct.Price,
			Quantity = newProduct.Quantity,
			IsActive = true
		};
		await _productRepository.AddAsync(product);
		return;
	}
}
