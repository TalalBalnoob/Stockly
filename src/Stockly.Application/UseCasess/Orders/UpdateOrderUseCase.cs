using Stockly.Application.DTOs.Orders;
using Stockly.Application.Exceptions;
using Stockly.Application.Interfaces.Repositories;
using Stockly.Application.Interfaces.UseCases.Orders;
using Stockly.Domain.Entities;

namespace Stockly.Application.UseCases.Orders;

public class UpdateOrderUseCase : IUpdateOrderUseCase {
	private readonly IOrdersRepo _orderRepository;
	private readonly IProductsRepo _productRepository;

	public UpdateOrderUseCase(IOrdersRepo orderRepository, IProductsRepo productRepository) {
		_orderRepository = orderRepository;
		_productRepository = productRepository;
	}

	public async Task ExecuteAsync(UpdateOrderRequest request) {
		// 1.Load the order with its current items, the entity comes back tracked
		// so every change we make here is picked up when the repo saves.
		Order order = await _orderRepository.GetByIdAsync(request.Id)
					  ?? throw new NotFoundException($"Order with id {request.Id} not found");

		// 2.Patch the plain fields, a null in the request means "leave it as it is"
		order.CustomerName = request.CustomerName ?? order.CustomerName;
		order.CustomerContact = request.CustomerContact ?? order.CustomerContact;
		order.Status = request.Status ?? order.Status;
		order.PaymentStatus = request.PaymentStatus ?? order.PaymentStatus;
		order.PaymentMethod = request.PaymentMethod ?? order.PaymentMethod;
		order.PaymentReference = request.PaymentReference ?? order.PaymentReference;
		order.ShippingAddress = request.ShippingAddress ?? order.ShippingAddress;
		order.CreatedAt = request.CreatedAt ?? order.CreatedAt;

		// 3.Sync the items, a null list means the caller did not touch them at all
		if (request.OrderItems is not null) {
			await SyncOrderItemsAsync(order, request.OrderItems);
		}

		// 4.The total is always derived from the items, request.Total is ignored
		order.Total = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);

		// 5.Save the updated order
		await _orderRepository.UpdateAsync(order.Id, order);
	}

	// Brings the stored items in line with the requested ones, matching on ProductId:
	// gone from the request -> removed, new in the request -> added,
	// still there -> quantity and price refreshed.
	private async Task SyncOrderItemsAsync(Order order, List<CreateOrderItemRequest> requestedItems) {
		var duplicate = requestedItems.GroupBy(i => i.ProductId).FirstOrDefault(g => g.Count() > 1);
		if (duplicate is not null) {
			throw new ArgumentException($"Product with id {duplicate.Key} is listed more than once, merge it into a single item");
		}

		var requestedProductIds = requestedItems.Select(i => i.ProductId).ToHashSet();

		// a.Drop the items that are not part of the order anymore
		var removedItems = order.OrderItems
			.Where(oi => !requestedProductIds.Contains(oi.ProductId))
			.ToList();

		foreach (var removedItem in removedItems) {
			order.OrderItems.Remove(removedItem);
		}

		// b.Add the new ones and refresh the ones that are still there
		foreach (var requestedItem in requestedItems) {
			if (requestedItem.Quantity < 1) {
				throw new ArgumentException($"Quantity for product with id {requestedItem.ProductId} must be at least 1");
			}

			Product product = await _productRepository.GetByIdAsync(requestedItem.ProductId)
							  ?? throw new NotFoundException($"Product with id {requestedItem.ProductId} not found");

			decimal price = requestedItem.CustomPrice != 0 ? requestedItem.CustomPrice : product.Price;
			OrderItem? existingItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == requestedItem.ProductId);

			if (existingItem is null) {
				order.OrderItems.Add(new OrderItem {
					OrderId = order.Id,
					ProductId = requestedItem.ProductId,
					Quantity = requestedItem.Quantity,
					Price = price
				});
				continue;
			}

			existingItem.Quantity = requestedItem.Quantity;
			existingItem.Price = price;
		}
	}
}
