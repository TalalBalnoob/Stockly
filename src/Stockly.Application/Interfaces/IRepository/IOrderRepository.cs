using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.IRepository;

public interface IOrderRepository {
	Task<IEnumerable<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(Guid id);
	Task<Order> AddAsync(Order order);
	Task<Order> UpdateAsync(Order order);
	Task DeleteAsync(Guid id);
}