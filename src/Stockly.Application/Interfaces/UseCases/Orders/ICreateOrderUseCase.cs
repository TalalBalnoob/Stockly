using Stockly.Application.DTOs.Orders;


namespace Stockly.Application.Interfaces.UseCases.Orders;

public interface ICreateOrderUseCase {
	Task<OrderResponseDto> ExecuteAsync(CreateOrderRequest request);
}
