using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class StockRepository(AppDbContext db) : IStockRepository {
	public async Task<IEnumerable<Stock>> GetAllAsync() {
		return db.Stocks.ToList();
	}

	public async Task<Stock?> GetByIdAsync(Guid id) {
		return await db.Stocks.FindAsync(id);
	}

	public async Task<Stock> AddAsync(Stock stock) {
		db.Stocks.Add(stock);
		await db.SaveChangesAsync();
		return stock;
	}

	public async Task<Stock> UpdateAsync(Stock stock) {
		db.Stocks.Update(stock);
		await db.SaveChangesAsync();
		return stock;
	}

	public async Task DeleteAsync(Guid id) {
		var stock = await GetByIdAsync(id);
		db.Stocks.Remove(stock);
		await db.SaveChangesAsync();
	}
}