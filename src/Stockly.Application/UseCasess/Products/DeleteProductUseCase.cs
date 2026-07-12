using Stockly.Application.Exceptions;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Products;

namespace Stockly.Application.UseCases.Products;

public class DeleteProductUseCase(IProductsRepo productsRepo) : IDeleteProductUseCase {
	public async Task ExecuteAsync(Guid productId) {
		var product = await productsRepo.GetByIdAsync(productId);
		if (product == null) {
			throw new NotFoundException($"Product with ID {productId} not found.");
		}

		await productsRepo.DeleteAsync(productId);
	}
}
