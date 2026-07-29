using Microsoft.EntityFrameworkCore;

using Stockly.Application.DTOs.Orders;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Data.Persistence;
using Stockly.Domain.Entities;
using Stockly.Domain.Enums;

namespace Stockly.Data.Repositories;

public class OrdersRepo : IOrdersRepo {
	private readonly StocklyDbContext _context;

	public OrdersRepo(StocklyDbContext context) {
		_context = context;
	}

	public async Task<IEnumerable<Order>> GetAllAsync() {
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.ToListAsync();
	}

	public async Task<Order?> GetByIdAsync(Guid id) {
		var order = await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.FirstOrDefaultAsync(o => o.Id == id);

		return order != null ? order : null;
	}

	public async Task<IEnumerable<Order>> GetByCustomerNameAsync(string customerName) {
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.Where(o => o.CustomerName != null && o.CustomerName.Contains(customerName))
			.ToListAsync();
	}

	public async Task<IEnumerable<Order>> GetByStatusAsync(Order_status status) {
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.Where(o => o.Status == status)
			.ToListAsync();
	}

	public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime start, DateTime end) {
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.Where(o => o.CreatedAt >= start && o.CreatedAt <= end)
			.ToListAsync();
	}

	public async Task<Order> AddAsync(Order order) {
		_context.Orders.Add(order);
		await _context.SaveChangesAsync();

		return order;
	}

	public async Task<Order> UpdateAsync(Guid id, Order order) {
		var existingOrder = await _context.Orders.FindAsync(id);
		if (existingOrder == null) {
			throw new KeyNotFoundException($"Order with ID {id} not found.");
		}

		await _context.SaveChangesAsync();

		return order;
	}

	public async Task DeleteAsync(Guid id) {
		var order = await _context.Orders.FindAsync(id);
		if (order != null) {
			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
		}
	}

	public async Task<bool> ExistsAsync(Guid id) {
		return await _context.Orders.AnyAsync(o => o.Id == id);
	}

	public OrderResponseDto MapToResponseDto(Order order) {
		return new OrderResponseDto {
			Id = order.Id,
			CustomerName = order.CustomerName,
			CustomerContact = order.CustomerContact,
			Status = order.Status,
			PaymentStatus = order.PaymentStatus,
			Total = order.Total,
			PaymentMethod = order.PaymentMethod,
			PaymentReference = order.PaymentReference,
			ShippingAddress = order.ShippingAddress,
			CreatedAt = order.CreatedAt,
			OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto {
				Id = oi.Id,
				ProductId = oi.ProductId,
				Quantity = oi.Quantity,
				Price = oi.Price
			}).ToList()
		};
	}
}
