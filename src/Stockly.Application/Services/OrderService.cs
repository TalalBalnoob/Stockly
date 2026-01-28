using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;
using Stockly.Domain.Enums;

namespace Stockly.Application.Services;

public class OrderService(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo) : IOrderService {
	public async Task<Order> GetById(Guid id) {
		return await orderRepo.GetByIdWithItemsAsync(id) ?? throw new Exception("Order Not Found");
	}

	public async Task<List<Order>> GetAll() {
		var orders = await orderRepo.GetAllWithItemsAsync();
		return orders.ToList();
	}

	public async Task<Order> SetOrderStatus(Guid orderId, OrderStatus newStatus) {
		var order = await orderRepo.GetByIdAsync(orderId)
		            ?? throw new Exception("Order not found");

		order.ChangeStatus(newStatus);

		return await orderRepo.UpdateAsync(order);
	}

	public async Task<Order> SetPaymentStatus(Guid orderId, PaymentStatus newStatus) {
		var order = await orderRepo.GetByIdAsync(orderId)
		            ?? throw new Exception("Order not found");

		order.ChangePaymentStatus(newStatus);

		return await orderRepo.UpdateAsync(order);
	}
}