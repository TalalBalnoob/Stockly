using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public class StockService(IStockRepository stockRepo, IProductRepository productRepo)
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
		var product = await productRepo.GetByIdAsync(productId);

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

		stockFromDb.ProductId = stockDto.ProductId;
		stockFromDb.StorageNote = stockDto.StorageNote ?? $"Product {stockDto.ProductId} has been updated";

		if (stockFromDb.Quantity != stockDto.Quantity) {
			stockFromDb.Quantity += stockDto.Quantity;

			adjustmentRepo.AddAsync(new StockAdjustment {
				ProductId = stockDto.ProductId,
				OrderId = Guid.Empty,
				Quantity = stockFromDb.Quantity,
				Reason = "Manual Stock adjustment"
			});
		}

		return await stockRepo.UpdateAsync(stockFromDb);
	}

	public async Task DeleteStock(Guid id) {
		var stock = await GetStockById(id);
		if (stock == null) throw new Exception("Stock not found");

		stockRepo.DeleteAsync(id);
	}
}