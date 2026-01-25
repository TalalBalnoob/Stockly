using Microsoft.EntityFrameworkCore;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Infrastructure.Repositories;

public class OrderRepository(AppDbContext db) : IOrderRepository {
	public async Task<IEnumerable<Order>> GetAllAsync() {
		return await db.Orders.ToListAsync();
	}

	public async Task<IEnumerable<Order>> GetAllWithItemsAsync() {
		return await db.Orders.Include(o => o.OrderItems).ToListAsync();
	}

	public async Task<Order?> GetByIdAsync(Guid id) {
		return await db.Orders.FindAsync(id);
	}


	public async Task<Order?> GetByIdWithItemsAsync(Guid id) {
		return await db.Orders
			.Include(o => o.OrderItems)
			.FirstOrDefaultAsync(o => o.Id == id);
	}

	public async Task<Order> AddAsync(Order order) {
		db.Orders.Add(order);
		await db.SaveChangesAsync();
		return order;
	}

	public async Task<Order> UpdateAsync(Order order) {
		db.Orders.Update(order);
		await db.SaveChangesAsync();
		return order;
	}

	public async Task DeleteAsync(Guid id) {
		var order = await GetByIdAsync(id);
		db.Orders.Remove(order);
		await db.SaveChangesAsync();
	}
}