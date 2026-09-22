using Stockly.Application.DTOs.Orders;
using Stockly.Application.DTOs.Products;
using Stockly.Application.DTOs.StockAdjustments;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Orders;
using Stockly.Application.Interfaces.UseCases.StockAdjustment;
using Stockly.Domain.Entities;

namespace Stockly.Application.UseCases.Orders;


public class CreateOrderUseCase : ICreateOrderUseCase {
	private readonly IOrdersRepo _orderRepository;
	private readonly IProductsRepo _productRepository;
	private readonly IStockAdjustmentsRepo _stockAdjustments;
	private readonly IAdjustStockUseCase _adjustStockUseCase;

	public CreateOrderUseCase(IOrdersRepo orderRepository, IProductsRepo productRepository, IStockAdjustmentsRepo stockAdjustments, IAdjustStockUseCase adjustStockUseCase) {
		_orderRepository = orderRepository;
		_productRepository = productRepository;
		_stockAdjustments = stockAdjustments;
		_adjustStockUseCase = adjustStockUseCase;
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
			Product product = await _productRepository.GetByIdAsync(newItem.ProductId)
										 ?? throw new Exception($"Product with id {newItem.ProductId} not found");

			if (product.Quantity < newItem.Quantity) throw new Exception($"out of range quantity");

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
		orderItems.ForEach((item) => {
			_adjustStockUseCase.ExecuteAsync(new CreateStockAdjustmentDto {
				ProductId = item.ProductId,
				Change = item.Quantity,
				Reason = "New order",
				RelatedOrderId = savedOrder.Id
			});
		});
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
