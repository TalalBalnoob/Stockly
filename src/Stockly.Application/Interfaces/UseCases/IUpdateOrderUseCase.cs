using Stockly.Application.UseCases.UpdateOrder;
using Stockly.Domain.Entity;

namespace Stockly.Application.Interfaces.UseCases;

public interface IUpdateOrderUseCase
{
    Task<Order> Execute(UpdateOrderDto orderDto);
}

