using Stockly.API.ApiDtos.OrderDtos;
using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Domain.Entity;
using Stockly.Domain.Enums;

namespace Stockly.Application.UseCases.CancelOrder;

public class ReturnOrderUseCase(
	IOrderService orderService,
	IStockService stockService,
	IOrderItemRepository orderItemRepo) : IReturnOrderUseCase {
	public async Task<Order> Execute(Guid id, CancelOrderDto dto) {
		orderService.SetOrderStatus(id, OrderStatus.Returned);
		var order = await orderService.GetById(id);

		if (dto.Restock) {
			var items = await orderItemRepo.GetAllByOrderIdAsync(id);

			foreach (var item in items) {
				stockService.UpdateStock(new UpdateStockDto {
					Quantity = item.Quantity,
					ProductId = item.ProductId,
					Reason = dto.Reason ?? "Order cancelled"
				});
			}
		}

		return order;
	}
}