using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Domain.Entity;

namespace Stockly.Application.Services;

public class OrderItemsService(
	IOrderItemRepository orderItemRepo,
	IProductRepository productRepo) : IOrderItemsService {
	public async Task<OrderItem> GetOrderItem(Guid id) {
		var item = await orderItemRepo.GetByIdAsync(id);
		if (item == null) throw new KeyNotFoundException("OrderItem not found");
		return item;
	}

	public async Task<List<OrderItem>> GetOrderItemsByOrderId(Guid id) {
		var items = await orderItemRepo.GetAllByOrderIdAsync(id);
		return items.ToList();
	}

	public async Task<OrderItem> CreateOrderItem(NewOrderItemDto orderItemDto) {
		decimal ItemPrice = orderItemDto.Price ?? productRepo.GetByIdAsync(orderItemDto.ProductId).Result.Price;
		var newItem = await orderItemRepo.AddAsync(new OrderItem {
			Id = new Guid(),
			ProductId = orderItemDto.ProductId,
			Quantity = orderItemDto.Quantity,
			OrderId = orderItemDto.OrderId,
			Price = ItemPrice
		});

		return newItem;
	}

	public async Task<OrderItem> UpdateOrderItem(UpdateOrderItemDto orderItemDto) {
		var itemFromDb = await GetOrderItem(orderItemDto.Id);
		if (itemFromDb == null) new Exception("Item not found");

		itemFromDb.Quantity = orderItemDto.Quantity ?? itemFromDb.Quantity;
		itemFromDb.Price = orderItemDto.Price ?? itemFromDb.Price;

		return await orderItemRepo.UpdateAsync(itemFromDb);
	}

	public async Task DeleteOrderItem(Guid id) {
		var item = await GetOrderItem(id);
		if (item == null) new Exception("Item not found");

		orderItemRepo.DeleteAsync(id);
	}
}