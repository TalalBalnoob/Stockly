using System.Collections.ObjectModel;

// using Microsoft.EntityFrameworkCore;

using Stockly.Application.DTOs.Orders;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Domain.Entities;
using Stockly.Domain.Enums;

namespace Stockly.Data.Repositories;

public class OrdersRepo : IOrdersRepo {
	private readonly Collection<Order> _context;

	public OrdersRepo(Collection<Order> context) {
		_context = context;
	}

	public async Task<IEnumerable<Order>> GetAllAsync() {
		return _context.ToList();
	}

	public async Task<Order?> GetByIdAsync(Guid id) {
		var order = _context.FirstOrDefault(o => o.Id == id);

		return order != null ? order : null;
	}

	public async Task<IEnumerable<Order>> GetByCustomerNameAsync(string customerName) {
		return _context.Where(o => o.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase));
	}

	public async Task<IEnumerable<Order>> GetByStatusAsync(Order_status status) {
		return _context.Where(o => o.Status == status).ToList();
	}

	public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime start, DateTime end) {
		return _context.Where(o => o.CreatedAt >= start && o.CreatedAt <= end).ToList();
	}

	public async Task<Order> AddAsync(Order order) {
		_context.Add(order);
		return order;
	}

	public async Task<Order> UpdateAsync(Guid id, Order order) {
		var existingOrder = _context.FirstOrDefault(o => o.Id == id);
		if (existingOrder == null) {
			throw new KeyNotFoundException($"Order with ID {id} not found.");
		}

		existingOrder = order;
		return order;
	}

	public async Task DeleteAsync(Guid id) {
		var order = _context.FirstOrDefault(o => o.Id == id);
		if (order != null) {
			_context.Remove(order);
		}
	}

	public async Task<bool> ExistsAsync(Guid id) {
		return _context.Any(o => o.Id == id);
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
