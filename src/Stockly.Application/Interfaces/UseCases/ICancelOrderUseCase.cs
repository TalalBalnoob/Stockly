using Stockly.API.ApiDtos.OrderDtos;
using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.UseCases;

public interface ICancelOrderUseCase {
	public Task<Order> Execute(Guid id, CancelOrderDto dto);
}