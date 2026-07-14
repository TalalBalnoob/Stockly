using Stockly.Application.DTOs.Orders;

namespace Stockly.Application.Interfaces.UseCases.Orders;

public interface IGetAllOrdersUseCase {
	Task<IEnumerable<OrderResponseDto>> ExecuteAsync();
}
