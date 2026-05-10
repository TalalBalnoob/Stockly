using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Application.UseCases;

public class DeleteProductUseCase(IProductRepository productRepository) {
	public async Task Execute(string id) {
		Product? productFromDb = await productRepository.GetByIdAsync(new Guid(id));
		if (productFromDb == null) throw new Exception("Product not found");

		await productRepository.DeleteAsync(productFromDb.Id);
		return;
	}
}
