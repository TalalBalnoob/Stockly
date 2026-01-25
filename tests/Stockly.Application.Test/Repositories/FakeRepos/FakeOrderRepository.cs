using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class FakeOrderRepository : IOrderRepository {
	private readonly List<Order> db;

	public FakeOrderRepository() {
		this.db = [];
	}

	public async Task<IEnumerable<Order>> GetAllAsync() {
		return db.ToList();
	}

	public async Task<IEnumerable<Order>> GetAllWithItemsAsync() {
		throw new NotImplementedException();
	}

	public async Task<Order?> GetByIdAsync(Guid id) {
		return db.Find(o => o.Id == id);
	}

	public async Task<Order?> GetByIdWithItemsAsync(Guid id) {
		throw new NotImplementedException();
	}

	public async Task<Order> AddAsync(Order order) {
		db.Add(order);
		return order;
	}

	public async Task<Order> UpdateAsync(Order order) {
		var index = db.FindIndex(o => o.Id == order.Id);
		if (index != -1) db[index] = order;
		return order;
	}

	public async void DeleteAsync(Guid id) {
		var order = await GetByIdAsync(id);
		db.Remove(order);
	}
}