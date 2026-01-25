using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public class OrderService(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo) : IOrderService {
	public async Task<Order> GetById(Guid id) {
		return await orderRepo.GetByIdWithItemsAsync(id) ?? throw new Exception("Order Not Found");
	}

	public async Task<List<Order>> GetAll() {
		var orders = await orderRepo.GetAllWithItemsAsync();
		return orders.ToList();
	}
}