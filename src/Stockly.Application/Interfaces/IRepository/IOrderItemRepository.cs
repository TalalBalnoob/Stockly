using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.IRepository;

public interface IOrderItemRepository {
	Task<IEnumerable<OrderItem>> GetAllAsync();
	Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(Guid orderId);
	Task<OrderItem?> GetByIdAsync(Guid id);
	Task<OrderItem> AddAsync(OrderItem orderItem);
	Task<OrderItem[]> AddRangeAsync(OrderItem[] orderItems);
	Task<OrderItem> UpdateAsync(OrderItem orderItem);
	Task DeleteAsync(Guid id);
}