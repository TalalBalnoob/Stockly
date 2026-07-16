using Stockly.Application.DTOs.Orders;

namespace Stockly.Application.Interfaces.UseCases.Orders;

public interface IGetOrderByIdUseCase {
	Task<OrderResponseDto> ExecuteAsync(Guid orderId);
}
