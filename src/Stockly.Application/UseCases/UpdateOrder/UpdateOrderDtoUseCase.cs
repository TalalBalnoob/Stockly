using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Application.UseCases.UpdateOrder;

public class UpdateOrderDtoUseCase(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo) {
	public async Task<Order> Execute(UpdateOrderDto orderDto) {
		var orderFromDb = await orderRepo.GetByIdWithItemsAsync(orderDto.Id);

		orderFromDb.CustomerName = orderDto.CustomerName ?? orderFromDb.CustomerName;
		orderFromDb.CustomerContact = orderDto.CustomerContact ?? orderFromDb.CustomerContact;
		orderFromDb.CustomerAddress = orderDto.CustomerAddress ?? orderFromDb.CustomerAddress;
		orderFromDb.PaymentDetails = orderDto.PaymentDetails ?? orderFromDb.PaymentDetails;
		orderFromDb.Note = orderDto.Note ?? orderFromDb.Note;
		orderFromDb.Status = orderDto.Status ?? orderFromDb.Status;

		// order items update (AI)
		var dtoItemIds = orderDto.OrderItems.Select(i => i.Id).ToHashSet();
		var dbItemIds = orderFromDb.OrderItems.Select(i => i.Id).ToHashSet();

		// Delete items that exist in DB but not in DTO
		var itemsToDelete = orderFromDb.OrderItems.Where(dbItem => !dtoItemIds.Contains(dbItem.Id)).ToList();
		foreach (var itemToDelete in itemsToDelete) {
			await orderItemRepo.DeleteAsync(itemToDelete.Id);
			orderFromDb.OrderItems.Remove(itemToDelete);
		}

		// Add new items or update existing ones
		foreach (var dtoItem in orderDto.OrderItems) {
			var existingItem = orderFromDb.OrderItems.FirstOrDefault(dbItem => dbItem.Id == dtoItem.Id);

			if (existingItem == null) {
				// Add new item
				var newItem = new OrderItem {
					Id = dtoItem.Id,
					OrderId = orderFromDb.Id,
					ProductId = dtoItem.ProductId ?? throw new Exception("ProductId is required for new items"),
					Quantity = dtoItem.Quantity ?? throw new Exception("Quantity is required for new items"),
					Price = dtoItem.Price ?? throw new Exception("Price is required for new items"),
					CreatedAt = DateTime.UtcNow
				};
				orderFromDb.OrderItems.Add(newItem);
				await orderItemRepo.AddAsync(newItem);
			}
			else {
				// Update existing item (only if details differ)
				bool hasChanges = false;

				if (dtoItem.Quantity.HasValue && existingItem.Quantity != dtoItem.Quantity.Value) {
					existingItem.Quantity = dtoItem.Quantity.Value;
					hasChanges = true;
				}

				if (dtoItem.Price.HasValue && existingItem.Price != dtoItem.Price.Value) {
					existingItem.Price = dtoItem.Price.Value;
					hasChanges = true;
				}

				if (hasChanges) {
					await orderItemRepo.UpdateAsync(existingItem);
				}
			}
		}

		// Recalculate total price
		orderFromDb.TotalPrice = orderFromDb.OrderItems.Sum(item => item.Price * item.Quantity);

		return await orderRepo.UpdateAsync(orderFromDb);
	}
}