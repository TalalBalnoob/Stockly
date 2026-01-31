using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class FakeStockAdjustmentRepository : IStockAdjustmentRepository {
	private readonly List<StockAdjustment> db;

	public FakeStockAdjustmentRepository() {
		this.db = [];
	}

	public async Task<IEnumerable<StockAdjustment>> GetAllAsync() {
		return db.ToList();
	}

	public async Task<StockAdjustment?> GetByIdAsync(Guid id) {
		return db.Find(s => id == s.Id);
	}

	public async Task<List<StockAdjustment>> GetByProductIdAsync(Guid productId) {
		return db.Where(s => s.ProductId == productId).ToList();
	}

	public async Task<StockAdjustment> AddAsync(StockAdjustment adjustment) {
		db.Add(adjustment);
		return adjustment;
	}

	public async Task<StockAdjustment> UpdateAsync(StockAdjustment adjustment) {
		var index = db.FindIndex(s => s.Id == adjustment.Id);
		if (index != -1) db[index] = adjustment;
		return adjustment;
	}

	public async void DeleteAsync(Guid id) {
		var adjustment = await GetByIdAsync(id);
		db.Remove(adjustment);
	}
}