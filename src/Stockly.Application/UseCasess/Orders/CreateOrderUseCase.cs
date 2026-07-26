using Stockly.Application.DTOs.Orders;
using Stockly.Application.DTOs.Products;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Domain.Entities;

namespace Stockly.Application.UseCases.Orders;

public class CreateOrderUseCase {
	private readonly IOrdersRepo _orderRepository;
	private readonly IProductsRepo _productRepository;

	public CreateOrderUseCase(IOrdersRepo orderRepository, IProductsRepo productRepository) {
		_orderRepository = orderRepository;
		_productRepository = productRepository;
	}

	public async Task<OrderResponseDto> ExecuteAsync(CreateOrderRequest request) {
		// 1.Create a new order entity
		var order = new Order {
			CustomerName = request.CustomerName,
			CustomerContact = request.CustomerContact,
			Status = request.Status,
			PaymentStatus = request.PaymentStatus,
			PaymentMethod = request.PaymentMethod,
			PaymentReference = request.PaymentReference,
			ShippingAddress = request.ShippingAddress,
			CreatedAt = request.CreatedAt
		};

		// 2.make order items from the product ids
		var orderItems = new List<OrderItem>();
		foreach (var newItem in request.OrderItems) {
			ProductResponseDto product = await _productRepository.GetByIdAsync(newItem.ProductId)
										 ?? throw new Exception($"Product with id {newItem.ProductId} not found");

			OrderItem orderItem = new OrderItem {
				ProductId = newItem.ProductId,
				Quantity = newItem.Quantity,
				Price = newItem.CustomPrice != 0 ? newItem.CustomPrice : product.Price
			};
			orderItems.Add(orderItem);
		}

		order.Total = orderItems.Sum(oi => oi.Price * oi.Quantity);

		// 3.Save the order to the database
		order.OrderItems = orderItems;
		var savedOrder = await _orderRepository.AddAsync(order);
		// 4.Return the order response dto

		return new OrderResponseDto {
			Id = order.Id,
			CustomerName = order.CustomerName,
			CustomerContact = order.CustomerContact,
			Status = order.Status,
			PaymentStatus = order.PaymentStatus,
			PaymentMethod = order.PaymentMethod,
			PaymentReference = order.PaymentReference,
			ShippingAddress = order.ShippingAddress,
			Total = order.Total,
			CreatedAt = order.CreatedAt,
			OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto {
				ProductId = oi.ProductId,
				Quantity = oi.Quantity,
				Price = oi.Price
			}).ToList()
		};
	}
}
