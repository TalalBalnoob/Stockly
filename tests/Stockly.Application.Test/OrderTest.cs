using NUnit.Framework;
using Stockly.Application.DTOs;
using Stockly.Application.UseCases.CreateNewOrder;
using Stockly.Application.UseCases.UpdateOrder;
using Stockly.Domain.Entity;
using Stockly.Domain.Enums;
using Stockly.Infrastructure.Repositories;
using Stockly.Infrastructure.Test;

namespace Stockly.Application.Test;

public class OrderTest {
	private CreateNewOrderUseCase _createOrderUseCase = null!;
	private UpdateOrderDtoUseCase _updateOrderUseCase = null!;
	private FakeOrderRepository _orderRepo = null!;
	private FakeOrderItemRepository _orderItemRepo = null!;
	private FakeProductRepository _productRepo = null!;
	private FakeStockRepository _stockRepository = null;
	private FakeStockAdjustmentRepository _stockAdjustmentRepository = null;

	[SetUp]
	public void Setup() {
		_orderRepo = new FakeOrderRepository();
		_orderItemRepo = new FakeOrderItemRepository();
		_productRepo = new FakeProductRepository();
		_stockRepository = new FakeStockRepository();
		_stockAdjustmentRepository = new FakeStockAdjustmentRepository();

		_createOrderUseCase =
			new CreateNewOrderUseCase(_orderRepo, _productRepo, _stockAdjustmentRepository, _stockRepository);
		_updateOrderUseCase = new UpdateOrderDtoUseCase(_orderRepo, _orderItemRepo);
	}

	[Test]
	public async Task CreateAndUpdateOrder_WithItemChanges_UpdatesCorrectly() {
		// Setup: Create products
		var product1 = new Product {
			Id = Guid.NewGuid(),
			Name = "Product 1",
			Price = 100,
			Stock = new Stock { Quantity = 50, Id = Guid.NewGuid() },
			IsActive = true,
		};
		var product2 = new Product {
			Id = Guid.NewGuid(),
			Name = "Product 2",
			Price = 200,
			Stock = new Stock { Quantity = 30, Id = Guid.NewGuid() },
			IsActive = true
		};
		var product3 = new Product {
			Id = Guid.NewGuid(),
			Name = "Product 3",
			Price = 150,
			Stock = new Stock { Quantity = 20, Id = Guid.NewGuid() },
			IsActive = true
		};

		await _productRepo.AddAsync(product1);
		await _productRepo.AddAsync(product2);
		await _productRepo.AddAsync(product3);

		// Step 1: Create order with 2 items
		var newOrderDto = new NewOrderDto {
			CustomerName = "John Doe",
			CustomerContact = "123456789",
			CustomerAddress = "123 Street",
			Status = OrderStatus.Processing,
			PaymentStatus = PaymentStatus.Unpaid,
			OrderItems = new List<NewOrderItemDto> {
				new NewOrderItemDto {
					ProductId = product1.Id,
					Quantity = 2,
					Price = 100
				},
				new NewOrderItemDto {
					ProductId = product2.Id,
					Quantity = 1,
					Price = 200
				}
			}
		};

		var createdOrder = await _createOrderUseCase.Execute(newOrderDto);

		// Verify order was created correctly
		Assert.That(createdOrder.OrderItems.Count, Is.EqualTo(2));
		Assert.That(createdOrder.TotalPrice, Is.EqualTo(400)); // 2*100 + 1*200

		var item1Id = createdOrder.OrderItems.First(i => i.ProductId == product1.Id).Id;
		var item2Id = createdOrder.OrderItems.First(i => i.ProductId == product2.Id).Id;

		// Step 2: Update order - delete item2, update item1, add new item3
		var updateOrderDto = new UpdateOrderDto {
			Id = createdOrder.Id,
			CustomerName = "John Doe Updated",
			OrderItems = new[] {
				new UpdateOrderItemDto {
					Id = item1Id,
					Quantity = 3, // Updated quantity
					Price = 95 // Updated price
				},
				new UpdateOrderItemDto {
					Id = Guid.NewGuid(), // New item
					ProductId = product3.Id,
					Quantity = 2,
					Price = 150
				}
				// item2 is not included, so it should be deleted
			}
		};

		var updatedOrder = await _updateOrderUseCase.Execute(updateOrderDto);

		// Verify updates
		Assert.That(updatedOrder.CustomerName, Is.EqualTo("John Doe Updated"));
		Assert.That(updatedOrder.OrderItems.Count, Is.EqualTo(2)); // 1 updated + 1 new = 2 total
		Assert.That(updatedOrder.TotalPrice, Is.EqualTo(585)); // 3*95 + 2*150 = 285 + 300

		// Verify item1 was updated
		var updatedItem1 = updatedOrder.OrderItems.First(i => i.Id == item1Id);
		Assert.That(updatedItem1.Quantity, Is.EqualTo(3));
		Assert.That(updatedItem1.Price, Is.EqualTo(95));
		Assert.That(updatedItem1.ProductId, Is.EqualTo(product1.Id)); // ProductId unchanged

		// Verify item2 was deleted
		Assert.That(updatedOrder.OrderItems.Any(i => i.Id == item2Id), Is.False);

		// Verify item3 was added
		var newItem3 = updatedOrder.OrderItems.First(i => i.ProductId == product3.Id);
		Assert.That(newItem3.Quantity, Is.EqualTo(2));
		Assert.That(newItem3.Price, Is.EqualTo(150));
	}
}