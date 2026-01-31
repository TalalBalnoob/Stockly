using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.UseCases;

namespace Stockly.Application.UseCases.DeleteOrder;

public class DeleteOrderUseCase(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo) : IDeleteOrderUseCase {
	public async Task Execute(Guid id) {
		var order = await orderRepo.GetByIdAsync(id)
		            ?? throw new Exception("Order not found");

		var items = await orderItemRepo.GetAllByOrderIdAsync(order.Id);

		foreach (var item in items) {
			await orderItemRepo.DeleteAsync(item.Id);
		}

		await orderRepo.DeleteAsync(order.Id);
	}
}