using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class FakeStockRepository : IStockRepository {
	private readonly List<Stock> db;

	public FakeStockRepository() {
		this.db = [];
	}

	public async Task<IEnumerable<Stock>> GetAllAsync() {
		return db.ToList();
	}

	public async Task<Stock?> GetByIdAsync(Guid id) {
		return db.Find(s => id == s.Id);
	}

	public async Task<Stock> AddAsync(Stock stock) {
		db.Add(stock);
		return stock;
	}

	public async Task<Stock> UpdateAsync(Stock stock) {
		var index = db.FindIndex(s => s.Id == stock.Id);
		if (index != -1) db[index] = stock;
		return stock;
	}

	public async Task DeleteAsync(Guid id) {
		var stock = await GetByIdAsync(id);
		db.Remove(stock);
	}
}