using Microsoft.EntityFrameworkCore;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class StockAdjustmentRepository(AppDbContext db) : IStockAdjustmentRepository {
	public async Task<IEnumerable<StockAdjustment>> GetAllAsync() {
		return db.StockAdjustments.ToList();
	}

	public async Task<StockAdjustment?> GetByIdAsync(Guid id) {
		return await db.StockAdjustments.FindAsync(id);
	}

	public async Task<List<StockAdjustment>> GetByProductIdAsync(Guid productId) {
		return await db.StockAdjustments.Where(s => s.ProductId == productId).ToListAsync();
	}

	public async Task<StockAdjustment> AddAsync(StockAdjustment adjustment) {
		db.StockAdjustments.Add(adjustment);
		await db.SaveChangesAsync();
		return adjustment;
	}

	public async Task<StockAdjustment> UpdateAsync(StockAdjustment adjustment) {
		db.StockAdjustments.Update(adjustment);
		await db.SaveChangesAsync();
		return adjustment;
	}

	public async void DeleteAsync(Guid id) {
		var adjustment = await GetByIdAsync(id);
		db.StockAdjustments.Remove(adjustment);
		await db.SaveChangesAsync();
	}
}