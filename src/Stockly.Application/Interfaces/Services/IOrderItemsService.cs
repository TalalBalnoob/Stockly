using Stockly.Application.DTOs;
using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.Services;

public interface IOrderItemsService {
	Task<OrderItem> GetOrderItem(Guid id);
	Task<List<OrderItem>> GetOrderItemsByOrderId(Guid id);
	Task<OrderItem> CreateOrderItem(NewOrderItemDto orderItemDto);
	Task<OrderItem> UpdateOrderItem(UpdateOrderItemDto orderItemDto);
	Task DeleteOrderItem(Guid id);
}