using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class FakeOrderItemRepository : IOrderItemRepository {
	private readonly List<OrderItem> db;

	public FakeOrderItemRepository() {
		this.db = [];
	}

	public async Task<IEnumerable<OrderItem>> GetAllAsync() {
		return db.ToList();
	}

	public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(Guid orderId) {
		throw new NotImplementedException();
	}

	public async Task<OrderItem?> GetByIdAsync(Guid id) {
		return db.Find(o => o.Id == id);
	}

	public async Task<OrderItem> AddAsync(OrderItem orderItem) {
		db.Add(orderItem);

		return orderItem;
	}

	public async Task<OrderItem[]> AddRangeAsync(OrderItem[] orderItems) {
		db.AddRange(orderItems);
		return orderItems;
	}

	public async Task<OrderItem> UpdateAsync(OrderItem orderItem) {
		var index = db.FindIndex(o => o.Id == orderItem.Id);
		if (index != -1) db[index] = orderItem;
		return orderItem;
	}

	public async Task DeleteAsync(Guid id) {
		var orderItem = await GetByIdAsync(id);
		db.Remove(orderItem);
	}
}