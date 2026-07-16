using Stockly.Application.DTOs.Orders;
using Stockly.Application.Exceptions;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Orders;

namespace Stockly.Application.UseCases.Orders;

public class GetOrderByIdUseCase(IOrdersRepo ordersRepo) : IGetOrderByIdUseCase {
	public async Task<OrderResponseDto> ExecuteAsync(Guid orderId) {
		OrderResponseDto order = await ordersRepo.GetByIdAsync(orderId);

		if (order == null) {
			throw new NotFoundException($"Order with ID {orderId} not found.");
		}

		return order;
	}
}
