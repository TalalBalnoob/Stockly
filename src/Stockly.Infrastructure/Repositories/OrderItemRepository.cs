using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class OrderItemRepository(AppDbContext db) : IOrderItemRepository {
	public async Task<IEnumerable<OrderItem>> GetAllAsync() {
		return db.OrderItems.ToList();
	}

	public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(Guid orderId) {
		return db.OrderItems.Where(o => o.OrderId == orderId).ToList();
	}

	public async Task<OrderItem?> GetByIdAsync(Guid id) {
		return await db.OrderItems.FindAsync(id);
	}

	public async Task<OrderItem> AddAsync(OrderItem orderItem) {
		db.OrderItems.Add(orderItem);
		await db.SaveChangesAsync();
		return orderItem;
	}

	public async Task<OrderItem[]> AddRangeAsync(OrderItem[] orderItems) {
		db.OrderItems.AddRange(orderItems);
		await db.SaveChangesAsync();
		return orderItems;
	}

	public async Task<OrderItem> UpdateAsync(OrderItem orderItem) {
		db.OrderItems.Update(orderItem);
		await db.SaveChangesAsync();
		return orderItem;
	}

	public async void DeleteAsync(Guid id) {
		var orderItem = await GetByIdAsync(id);
		db.OrderItems.Remove(orderItem);
		await db.SaveChangesAsync();
	}
}