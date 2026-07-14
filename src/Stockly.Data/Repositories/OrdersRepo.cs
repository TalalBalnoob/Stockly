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

	public async Task<IEnumerable<OrderResponseDto>> GetAllAsync() {
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.Select(o => MapToResponseDto(o))
			.ToListAsync();
	}

	public async Task<OrderResponseDto?> GetByIdAsync(Guid id) {
		var order = await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.FirstOrDefaultAsync(o => o.Id == id);

		return order != null ? MapToResponseDto(order) : null;
	}

	public async Task<IEnumerable<OrderResponseDto>> GetByCustomerNameAsync(string customerName) {
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.Where(o => o.CustomerName != null && o.CustomerName.Contains(customerName))
			.Select(o => MapToResponseDto(o))
			.ToListAsync();
	}

	public async Task<IEnumerable<OrderResponseDto>> GetByStatusAsync(Order_status status) {
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.Where(o => o.Status == status)
			.Select(o => MapToResponseDto(o))
			.ToListAsync();
	}

	public async Task<IEnumerable<OrderResponseDto>> GetByDateRangeAsync(DateTime start, DateTime end) {
		return await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.Where(o => o.CreatedAt >= start && o.CreatedAt <= end)
			.Select(o => MapToResponseDto(o))
			.ToListAsync();
	}

	public async Task<OrderResponseDto> AddAsync(CreateOrderDto dto) {
		var order = new Order {
			Id = Guid.NewGuid(),
			CustomerName = dto.CustomerName,
			CustomerContact = dto.CustomerContect,
			PaymentMethod = dto.PaymentMethod,
			ShippingAddress = dto.ShippingAddress,
			Status = Order_status.Pending,
			PaymentStatus = Payment_status.Pending,
			CreatedAt = DateTime.UtcNow,
			OrderItems = new List<OrderItem>()
		};

		decimal total = 0;

		foreach (var itemDto in dto.OrderItems) {
			var product = await _context.Products.FindAsync(itemDto.ProductId);
			if (product == null) {
				throw new KeyNotFoundException($"Product with ID {itemDto.ProductId} not found.");
			}

			var orderItem = new OrderItem {
				Id = Guid.NewGuid(),
				ProductId = itemDto.ProductId,
				OrderId = order.Id,
				Quantity = itemDto.Quantity,
				Price = product.Price,
				CreatedAt = DateTime.UtcNow,
				Product = product
			};

			order.OrderItems.Add(orderItem);
			total += orderItem.Price * orderItem.Quantity;
		}

		order.Total = total;

		_context.Orders.Add(order);
		await _context.SaveChangesAsync();

		return MapToResponseDto(order);
	}

	public async Task<OrderResponseDto> UpdateAsync(Guid id, UpdateOrderDto dto) {
		var order = await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.FirstOrDefaultAsync(o => o.Id == id);

		if (order == null) {
			throw new KeyNotFoundException($"Order with ID {id} not found.");
		}

		if (dto.CustomerName != null) order.CustomerName = dto.CustomerName;
		if (dto.CustomerContact != null) order.CustomerContact = dto.CustomerContact;
		if (dto.Status.HasValue) order.Status = dto.Status.Value;
		if (dto.PaymentStatus.HasValue) order.PaymentStatus = dto.PaymentStatus.Value;
		if (dto.PaymentMethod != null) order.PaymentMethod = dto.PaymentMethod;
		if (dto.PaymentReference != null) order.PaymentReference = dto.PaymentReference;
		if (dto.ShippingAddress != null) order.ShippingAddress = dto.ShippingAddress;

		await _context.SaveChangesAsync();

		return MapToResponseDto(order);
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

	private static OrderResponseDto MapToResponseDto(Order order) {
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
				ProductName = oi.Product?.Name ?? "Unknown Product",
				Quantity = oi.Quantity,
				Price = oi.Price
			}).ToList()
		};
	}
}
