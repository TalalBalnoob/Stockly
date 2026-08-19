using Stockly.Application.DTOs.Orders;

namespace Stockly.Application.Interfaces.UseCases.Orders;

public interface IUpdateOrderUseCase {
	Task ExecuteAsync(UpdateOrderRequest request);
}
