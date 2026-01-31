using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.IRepository;

public interface IStockAdjustmentRepository {
	Task<IEnumerable<StockAdjustment>> GetAllAsync();
	Task<StockAdjustment?> GetByIdAsync(Guid id);
	Task<List<StockAdjustment>> GetByProductIdAsync(Guid productId);
	Task<StockAdjustment> AddAsync(StockAdjustment adjustment);
	Task<StockAdjustment> UpdateAsync(StockAdjustment adjustment);
	void DeleteAsync(Guid id);
}