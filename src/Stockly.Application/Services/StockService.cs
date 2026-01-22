using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public interface IStockService {
	Task<IEnumerable<Stock>> GetAllStocks();
	Task<Stock?> GetStockById(Guid id);
	Task<Stock?> GetStockByProductId(Guid productId);
	Task<Stock> AddNewStock(NewStockDto stock);
	Task<Stock> UpdateStock(UpdateStockDto stockDto);
	void DeleteStock(Guid id);
}

public class StockService(IStockRepository stockRepo, IProductRepository productRepo, IProductService productService)
	: IStockService {
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

	public async Task<Stock> AddNewStock(NewStockDto stock) {
		var newStock = await stockRepo.AddAsync(new() {
			ProductId = stock.ProductId,
			Quantity = stock.InialQuantity,
			StorageNote = stock.StorageNote ?? $"Product {stock.ProductId} has been added",
		});

		return newStock;
	}

	public async Task<Stock> UpdateStock(UpdateStockDto stockDto) {
		var stockFromDb = await GetStockByProductId(stockDto.Id);
		if (stockFromDb == null) throw new Exception("Stock not found");

		stockFromDb.Quantity = stockDto.InialQuantity;
		stockFromDb.ProductId = stockDto.ProductId;
		stockFromDb.StorageNote = stockDto.StorageNote ?? $"Product {stockDto.ProductId} has been updated";

		return await stockRepo.UpdateAsync(stockFromDb);
	}

	public async void DeleteStock(Guid id) {
		var stock = await GetStockById(id);
		if (stock == null) throw new Exception("Stock not found");

		stockRepo.DeleteAsync(id);
	}
}