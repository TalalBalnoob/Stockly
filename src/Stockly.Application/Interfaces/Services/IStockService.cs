using Stockly.Application.DTOs;
using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.Services;

public interface IStockService {
	Task<IEnumerable<Stock>> GetAllStocks();
	Task<Stock?> GetStockById(Guid id);
	Task<Stock?> GetStockByProductId(Guid productId);
	Task<Stock> AddNewStock(NewStockDto stock);
	Task<Stock> UpdateStock(UpdateStockDto stockDto);
	void DeleteStock(Guid id);
}