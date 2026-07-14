using Stockly.Application.DTOs.Orders;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Orders;

namespace Stockly.Application.UseCases.Orders;

public class GetAllOrdersUseCase : IGetAllOrdersUseCase {
	private readonly IOrdersRepo _orderRepository;

	public GetAllOrdersUseCase(IOrdersRepo orderRepository) {
		_orderRepository = orderRepository;
	}

	public async Task<IEnumerable<OrderResponseDto>> ExecuteAsync() {
		IEnumerable<OrderResponseDto> orders = await _orderRepository.GetAllAsync();
		return orders.Select(order => new OrderResponseDto {
			Id = order.Id,
			Status = order.Status,
			CustomerName = order.CustomerName,
			CustomerContact = order.CustomerContact,
			PaymentMethod = order.PaymentMethod,
			PaymentReference = order.PaymentReference,
			PaymentStatus = order.PaymentStatus,
			ShippingAddress = order.ShippingAddress,
			Total = order.Total,
			CreatedAt = order.CreatedAt,
			OrderItems = order.OrderItems.Select(item => new OrderItemResponseDto {
				Id = item.Id,
				ProductId = item.ProductId,
				Quantity = item.Quantity,
				Price = item.Price,
			}).ToList()
		});
	}
}
