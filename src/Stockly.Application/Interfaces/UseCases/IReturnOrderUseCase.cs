using Stockly.API.ApiDtos.OrderDtos;
using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.UseCases;

public interface IReturnOrderUseCase {
	public Task<Order> Execute(Guid id, CancelOrderDto dto);
}