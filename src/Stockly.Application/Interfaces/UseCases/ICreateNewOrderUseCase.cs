using Stockly.Application.DTOs;
using Stockly.Domain.Entity;
using Stockly.Application.UseCases.CreateProductWithStock;

public interface ICreateNewOrderUseCase {
	Task<Order> Execute(NewOrderDto orderDto);
}