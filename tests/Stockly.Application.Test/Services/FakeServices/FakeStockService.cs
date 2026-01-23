using Stockly.Application.DTOs;
using Stockly.Application.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Test.Services.FakeServices;

public class FakeStockService : IStockService {
	public List<Stock> Stocks = [];

	public async Task<IEnumerable<Stock>> GetAllStocks() {
		throw new NotImplementedException();
	}

	public async Task<Stock?> GetStockById(Guid id) {
		throw new NotImplementedException();
	}

	public async Task<Stock?> GetStockByProductId(Guid productId) {
		throw new NotImplementedException();
	}

	public Task<Stock> AddNewStock(NewStockDto dto) {
		var stock = new Stock {
			Id = Guid.NewGuid(),
			ProductId = dto.ProductId,
			Quantity = dto.InialQuantity
		};

		Stocks.Add(stock);
		return Task.FromResult(stock);
	}

	public async Task<Stock> UpdateStock(UpdateStockDto stockDto) {
		throw new NotImplementedException();
	}


	public Task DeleteStock(Guid productId) {
		Stocks.RemoveAll(s => s.ProductId == productId);
		return Task.CompletedTask;
	}
}