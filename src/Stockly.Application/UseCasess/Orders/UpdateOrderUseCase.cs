using Stockly.Application.DTOs.Orders;
using Stockly.Application.Exceptions;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Orders;

namespace Stockly.Application.UseCases.Orders;

public class UpdateOrderUseCase : IUpdateOrderUseCase {
	private readonly IOrdersRepo _orderRepository;

	public UpdateOrderUseCase(IOrdersRepo orderRepository) {
		_orderRepository = orderRepository;
	}

	public async Task ExecuteAsync(UpdateOrderRequest request) {
		var order = await _orderRepository.GetByIdAsync(request.Id);
		if (order == null) {
			throw new NotFoundException($"{request.Id} Order not found");
		}

		await _orderRepository.UpdateAsync(order.Id, order);
	}
}

