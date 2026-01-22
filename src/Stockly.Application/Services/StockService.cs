using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public class StockService(IStockRepository stockRepo, IProductRepository productRepo, IProductService productService) {
	public async Task<IEnumerable<Stock>> GetAllStocks() {
		return await stockRepo.GetAllAsync();
	}

	public async Task<Stock?> GetStockById(Guid id) {
		var stock = await stockRepo.GetByIdAsync(id);
		if (stock == null) throw new Exception("Stock not found");

		return stock;
	}

	public async Task<Stock?> GetStockByProductId(Guid productId) {
		var product = await productService.GetProductById(productId);

		var stock = await stockRepo.GetByIdAsync(product.StockId);
		return stock;
	}
}