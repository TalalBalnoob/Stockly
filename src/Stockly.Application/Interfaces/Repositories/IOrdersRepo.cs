using Stockly.Application.DTOs.Orders;
using Stockly.Domain.Entities;
using Stockly.Domain.Enums;

namespace Stockly.Application.Interfaces.Repositories;

public interface IOrdersRepo {
	// Queries
	Task<IEnumerable<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(Guid id);
	Task<IEnumerable<Order>> GetByCustomerNameAsync(string customerName);
	Task<IEnumerable<Order>> GetByStatusAsync(Order_status status);
	Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime start, DateTime end);

	// Commands
	Task<Order> AddAsync(Order order);
	Task<Order> UpdateAsync(Guid id, Order order);
	Task DeleteAsync(Guid id);

	// Checks
	Task<bool> ExistsAsync(Guid id);

	abstract OrderResponseDto MapToResponseDto(Order order);
}
