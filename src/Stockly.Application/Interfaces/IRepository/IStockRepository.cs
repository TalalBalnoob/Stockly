using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.IRepository;

public interface IStockRepository {
	Task<IEnumerable<Stock>> GetAllAsync();
	Task<Stock?> GetByIdAsync(Guid id);
	Task<Stock> AddAsync(Stock stock);
	Task<Stock> UpdateAsync(Stock stock);
	void DeleteAsync(Guid id);
}