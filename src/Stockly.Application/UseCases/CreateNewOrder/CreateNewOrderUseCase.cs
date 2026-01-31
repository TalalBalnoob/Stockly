using Stockly.Application.DTOs;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Domain.Entity;

namespace Stockly.Application.UseCases.CreateNewOrder;

public class CreateNewOrderUseCase(
	IOrderRepository orderRepo,
	IProductRepository productRepo,
	IStockAdjustmentRepository adjustmentRepo,
	IStockRepository stockRepo) {
	public async Task<Order> Execute(NewOrderDto orderDto) {
		var newOrder = new Order {
			Id = Guid.NewGuid(),
			CustomerName = orderDto.CustomerName,
			CustomerAddress = orderDto.CustomerAddress,
			CustomerContact = orderDto.CustomerContact,
			PaymentDetails = orderDto.PaymentDetails,
			PaymentStatus = orderDto.PaymentStatus,
			Status = orderDto.Status,
			Note = orderDto.Note,
			CreatedAt = DateTime.UtcNow
		};

		foreach (NewOrderItemDto orderItem in orderDto.OrderItems) {
			var product = await productRepo.GetByIdWithStockAsync(orderItem.ProductId);
			if (product == null) throw new Exception("Product not found");
			if (orderItem.Quantity < 1 || product.Stock.Quantity < orderItem.Quantity)
				throw new Exception("Insufficient stock");

			var newItem = new OrderItem {
				Id = Guid.NewGuid(),
				ProductId = orderItem.ProductId,
				Quantity = orderItem.Quantity,
				OrderId = newOrder.Id,
				Price = orderItem.Price ?? product.Price,
				CreatedAt = DateTime.UtcNow
			};

			newOrder.OrderItems.Add(newItem);

			newOrder.TotalPrice += newItem.Price * newItem.Quantity;

			var stockUpdate = product.Stock;
			stockUpdate.Quantity = product.Stock.Quantity - orderItem.Quantity;
			await stockRepo.UpdateAsync(stockUpdate);

			await adjustmentRepo.AddAsync(new StockAdjustment {
				ProductId = orderItem.ProductId,
				OrderId = newOrder.Id,
				Quantity = -orderItem.Quantity,
				Reason = "Order created"
			});
		}

		return await orderRepo.AddAsync(newOrder);
	}
}