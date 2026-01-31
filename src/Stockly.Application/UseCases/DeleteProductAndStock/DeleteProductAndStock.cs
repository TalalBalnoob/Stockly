using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.UseCases;

namespace Stockly.Application.UseCases.DeleteProductAndStock;

public class DeleteProductAndStock(IStockRepository stockRepo, IProductRepository productRepo)
	: IDeleteProductAndStock {
	public async Task Execute(Guid id) {
		var product = await productRepo.GetByIdAsync(id);
		if (product == null) throw new Exception("Product not found");

		await stockRepo.DeleteAsync(product.StockId);
		await productRepo.DeleteAsync(id);
	}
}