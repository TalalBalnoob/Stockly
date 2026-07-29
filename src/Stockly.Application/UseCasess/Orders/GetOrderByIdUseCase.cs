using Stockly.Application.DTOs.Orders;
using Stockly.Application.Exceptions;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Orders;
using Stockly.Domain.Entities;

namespace Stockly.Application.UseCases.Orders;

public class GetOrderByIdUseCase(IOrdersRepo ordersRepo) : IGetOrderByIdUseCase {
	public async Task<OrderResponseDto> ExecuteAsync(Guid orderId) {
		Order order = await ordersRepo.GetByIdAsync(orderId);

		if (order == null) {
			throw new NotFoundException($"Order with ID {orderId} not found.");
		}

		return ordersRepo.MapToResponseDto(order);
	}
}
